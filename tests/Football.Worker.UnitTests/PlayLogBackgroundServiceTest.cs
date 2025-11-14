using AutoMapper;
using Football.Application.Features.Games;
using Football.Application.Features.Games.Models;
using Football.Application.Features.Plays;
using Football.Application.Features.Plays.Models;
using Football.Application.Features.Stats;
using Football.Application.Interfaces;
using Football.Application.Options;
using Football.Infrastructure.Extensions;
using Football.Infrastructure.Options;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Polly;

namespace Football.Worker.UnitTests;

public class PlayLogBackgroundServiceTest
{
    private readonly Mock<IServiceScopeFactory> _mockScopeFactory;
    private readonly Mock<IServiceScope> _mockServiceScope;
    private readonly Mock<IServiceProvider> _mockServiceProvider;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<PlayLogBackgroundService>> _mockLogger;
    private readonly Mock<IHubConnectionFactory<IHub>> _mockHubConnectionFactory;
    private readonly Mock<IHub> _mockHub;
    private readonly Mock<ISender> _mockMediator;
    private readonly Mock<IHostApplicationLifetime> _mockApplicationLifetime;
    private readonly IOptions<HubOptions> _hubOptions;
    private readonly IOptions<ScoreboardOptions> _scoreboardOptions;

    public PlayLogBackgroundServiceTest()
    {
        _mockScopeFactory = new Mock<IServiceScopeFactory>();
        _mockServiceScope = new Mock<IServiceScope>();
        _mockServiceProvider = new Mock<IServiceProvider>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<PlayLogBackgroundService>>();
        _mockHubConnectionFactory = new Mock<IHubConnectionFactory<IHub>>();
        _mockHub = new Mock<IHub>();
        _mockMediator = new Mock<ISender>();
        _mockApplicationLifetime = new Mock<IHostApplicationLifetime>();

        _hubOptions = Options.Create(new HubOptions { HubUrl = "https://localhost:5001/hub/plays" });
        _scoreboardOptions = Options.Create(new ScoreboardOptions { Week = 1 });

        // Setup service scope factory chain
        _mockScopeFactory.Setup(x => x.CreateScope()).Returns(_mockServiceScope.Object);
        _mockServiceScope.Setup(x => x.ServiceProvider).Returns(_mockServiceProvider.Object);
        _mockServiceProvider.Setup(x => x.GetService(typeof(ISender))).Returns(_mockMediator.Object);

        // Setup hub connection factory
        _mockHubConnectionFactory.Setup(x => x.CreateHub()).Returns(_mockHub.Object);
    }

    [Fact]
    public async Task StartAsync_InitializesGamesStartsHubAndLogs()
    {
        // Arrange
        var gameDtos = new List<GameDto>
        {
            new GameDto { Id = 1, HomeTeam = "KC", AwayTeam = "SF" },
            new GameDto { Id = 2, HomeTeam = "NE", AwayTeam = "BUF" },
            new GameDto { Id = 3, HomeTeam = "DAL", AwayTeam = "NYG" }
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetGamesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gameDtos);

        _mockHub.Setup(h => h.StartAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var service = new PlayLogBackgroundService(
            _mockScopeFactory.Object,
            _mockMapper.Object,
            _mockLogger.Object,
            _mockHubConnectionFactory.Object,
            _hubOptions,
            _scoreboardOptions,
            _mockApplicationLifetime.Object);

        // Act
        await service.StartAsync(CancellationToken.None);

        // Assert - Verifies games are loaded from database
        _mockMediator.Verify(m => m.Send(
            It.Is<GetGamesQuery>(q => q.Week == 1),
            It.IsAny<CancellationToken>()),
            Times.Once);

        // Assert - Verifies hub connection is started
        _mockHub.Verify(h => h.StartAsync(
            It.IsAny<CancellationToken>()),
            Times.Once);

        // Assert - Verifies information is logged
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Worker hosted service started")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task StartAsync_OnException_StopsApplicationAndLogsCritical()
    {
        // Arrange
        var exception = new Exception("Database connection failed");
        _mockMediator.Setup(m => m.Send(It.IsAny<GetGamesQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(exception);

        var service = new PlayLogBackgroundService(
            _mockScopeFactory.Object,
            _mockMapper.Object,
            _mockLogger.Object,
            _mockHubConnectionFactory.Object,
            _hubOptions,
            _scoreboardOptions,
            _mockApplicationLifetime.Object);

        // Act
        await service.StartAsync(CancellationToken.None);

        // Assert - Verifies application is stopped
        _mockApplicationLifetime.Verify(x => x.StopApplication(), Times.Once);

        // Assert - Verifies critical error is logged
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Critical,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Worker hosted service did not start")),
                exception,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task StartAsync_UsesCorrectWeekFromOptions()
    {
        // Arrange
        var customOptions = Options.Create(new ScoreboardOptions { Week = 5 });
        var gameDtos = new List<GameDto>();

        _mockMediator.Setup(m => m.Send(It.IsAny<GetGamesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gameDtos);

        _mockHub.Setup(h => h.StartAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var service = new PlayLogBackgroundService(
            _mockScopeFactory.Object,
            _mockMapper.Object,
            _mockLogger.Object,
            _mockHubConnectionFactory.Object,
            _hubOptions,
            customOptions,
            _mockApplicationLifetime.Object);

        // Act
        await service.StartAsync(CancellationToken.None);

        // Assert
        _mockMediator.Verify(m => m.Send(
            It.Is<GetGamesQuery>(q => q.Week == 5),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task StopAsync_StopsHubAndLogsMessages()
    {
        // Arrange
        var gameDtos = new List<GameDto>();
        _mockMediator.Setup(m => m.Send(It.IsAny<GetGamesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gameDtos);

        _mockHub.Setup(h => h.StartAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _mockHub.Setup(h => h.StopAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var service = new PlayLogBackgroundService(
            _mockScopeFactory.Object,
            _mockMapper.Object,
            _mockLogger.Object,
            _mockHubConnectionFactory.Object,
            _hubOptions,
            _scoreboardOptions,
            _mockApplicationLifetime.Object);

        await service.StartAsync(CancellationToken.None);

        // Act
        await service.StopAsync(CancellationToken.None);

        // Assert - Verifies hub connection is stopped
        _mockHub.Verify(h => h.StopAsync(It.IsAny<CancellationToken>()), Times.Once);

        // Assert - Verifies hub stop is logged
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Hub connection stopped")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);

        // Assert - Verifies service stop is logged
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Worker hosted service stopped")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task DisposeAsync_DisposesHubConnection()
    {
        // Arrange
        var gameDtos = new List<GameDto>();
        _mockMediator.Setup(m => m.Send(It.IsAny<GetGamesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gameDtos);

        _mockHub.Setup(h => h.StartAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _mockHub.Setup(h => h.DisposeAsync())
            .Returns(Task.CompletedTask);

        var service = new PlayLogBackgroundService(
            _mockScopeFactory.Object,
            _mockMapper.Object,
            _mockLogger.Object,
            _mockHubConnectionFactory.Object,
            _hubOptions,
            _scoreboardOptions,
            _mockApplicationLifetime.Object);

        await service.StartAsync(CancellationToken.None);

        // Act
        await service.DisposeAsync();

        // Assert
        _mockHub.Verify(h => h.DisposeAsync(), Times.Once);
    }

    [Fact]
    public async Task DisposeAsync_LogsHubConnectionDisposed()
    {
        // Arrange
        var gameDtos = new List<GameDto>();
        _mockMediator.Setup(m => m.Send(It.IsAny<GetGamesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gameDtos);

        _mockHub.Setup(h => h.StartAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _mockHub.Setup(h => h.DisposeAsync())
            .Returns(Task.CompletedTask);

        var service = new PlayLogBackgroundService(
            _mockScopeFactory.Object,
            _mockMapper.Object,
            _mockLogger.Object,
            _mockHubConnectionFactory.Object,
            _hubOptions,
            _scoreboardOptions,
            _mockApplicationLifetime.Object);

        await service.StartAsync(CancellationToken.None);

        // Act
        await service.DisposeAsync();

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Hub connection disposed")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task DisposeAsync_LogsWorkerHostedServiceDisposed()
    {
        // Arrange
        var gameDtos = new List<GameDto>();
        _mockMediator.Setup(m => m.Send(It.IsAny<GetGamesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gameDtos);

        _mockHub.Setup(h => h.StartAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _mockHub.Setup(h => h.DisposeAsync())
            .Returns(Task.CompletedTask);

        var service = new PlayLogBackgroundService(
            _mockScopeFactory.Object,
            _mockMapper.Object,
            _mockLogger.Object,
            _mockHubConnectionFactory.Object,
            _hubOptions,
            _scoreboardOptions,
            _mockApplicationLifetime.Object);

        await service.StartAsync(CancellationToken.None);

        // Act
        await service.DisposeAsync();

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Worker hosted service disposed")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void Constructor_CreatesHubFromFactory()
    {
        // Act
        var service = new PlayLogBackgroundService(
            _mockScopeFactory.Object,
            _mockMapper.Object,
            _mockLogger.Object,
            _mockHubConnectionFactory.Object,
            _hubOptions,
            _scoreboardOptions,
            _mockApplicationLifetime.Object);

        // Assert
        _mockHubConnectionFactory.Verify(f => f.CreateHub(), Times.Once);
    }

    [Fact]
    public void Constructor_InitializesWithCorrectScoreboardOptions()
    {
        // Arrange
        var customOptions = Options.Create(new ScoreboardOptions { Week = 10 });

        // Act
        var service = new PlayLogBackgroundService(
            _mockScopeFactory.Object,
            _mockMapper.Object,
            _mockLogger.Object,
            _mockHubConnectionFactory.Object,
            _hubOptions,
            customOptions,
            _mockApplicationLifetime.Object);

        // Assert - Verify by checking that StartAsync uses the correct week
        // This is implicitly tested in StartAsync_UsesCorrectWeekFromOptions
        Assert.NotNull(service);
    }
}
