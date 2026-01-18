using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Football.Application.Features.Games.Models;
using Football.Application.Features.Plays.Models;
using Football.Application.Features.Stats.Models;
using Football.Application.Interfaces;
using Football.Blazor.Components;
using Football.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Polly;
using RichardSzalay.MockHttp;
using GameCastPage = Football.Blazor.Pages.GameCast;

namespace Football.Blazor.UnitTests.Pages;

public class GameCastTests : TestContext
{
    private readonly MockHttpMessageHandler mockHttp;
    private readonly Mock<IHubFactory<IHub>> mockHubFactory;
    private readonly Mock<IHub> mockHub;
    private readonly Mock<IOptions<HubOptions>> mockHubOptions;
    private readonly Mock<ILogger<Blazor.Components.GameComponentBase>> mockLogger;

    public GameCastTests()
    {
        mockHttp = new MockHttpMessageHandler();

        HttpClient httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri("http://localhost");

        var mockHttpClientFactory = new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
        Services.AddSingleton(mockHttpClientFactory.Object);

        mockHubFactory = new Mock<IHubFactory<IHub>>();
        mockHub = new Mock<IHub>();
        mockHubOptions = new Mock<IOptions<HubOptions>>();
        mockLogger = new Mock<ILogger<Blazor.Components.GameComponentBase>>();

        // Setup default hub options
        mockHubOptions.Setup(x => x.Value).Returns(new HubOptions
        {
            HubUrl = "http://localhost:5000/hub",
            RetryOptions = new RetryOptions
            {
                MaxRetryAttempts = 3,
                BackoffType = DelayBackoffType.Constant
            }
        });

        // Setup Hub factory to return Mock Hub
        mockHubFactory.Setup(x => x.CreateHub()).Returns(mockHub.Object);

        // Setup hub methods to return a disposable
        mockHub.Setup(x => x.On(It.IsAny<string>(), It.IsAny<Action<PlayDto>>()))
            .Returns(Mock.Of<IDisposable>());
        mockHub.Setup(x => x.StartAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Register services
        Services.AddSingleton(mockHubFactory.Object);
        Services.AddSingleton(mockHubOptions.Object);
        Services.AddSingleton(mockLogger.Object);
    }

    [Fact]
    public void GameCast_HappyPath_RendersGameAndStats()
    {
        // Arrange
        var gameId = 1;

        var game = new GameDto
        {
            Id = gameId,
            Week = 1,
            HomeTeam = "Chiefs",
            HomeScore = 21,
            AwayTeam = "Bills",
            AwayScore = 17,
            State = "InProgress",
            Quarter = 3,
            QuarterSecondsRemaining = 600
        };

        var gameStats = new GameStatDto
        {
            GameId = gameId,
            Stats = new List<StatDto>
            {
                new StatDto
                {
                    Team = "Chiefs",
                    Home = true,
                    Score = 21,
                    PassingYards = 250,
                    Sacks = 2,
                    Punts = 3,
                    ReturnYards = 50
                },
                new StatDto
                {
                    Team = "Bills",
                    Home = false,
                    Score = 17,
                    PassingYards = 200,
                    Sacks = 1,
                    Punts = 4,
                    ReturnYards = 30
                }
            }
        };

        mockHttp.When($"*/games/{gameId}")
            .Respond("application/json", System.Text.Json.JsonSerializer.Serialize(game));

        mockHttp.When($"*/games/{gameId}/stats")
            .Respond("application/json", System.Text.Json.JsonSerializer.Serialize(gameStats));

        // Act
        var cut = RenderComponent<GameCastPage>(parameters => parameters
            .Add(p => p.GameId, gameId));

        // Assert
        cut.WaitForState(() => cut.FindComponents<Blazor.Components.GameCard>().Count == 1, TimeSpan.FromSeconds(2));

        // Verify game title is displayed
        cut.Markup.Should().Contain("Bills @ Chiefs");

        // Verify Back link is rendered
        var backLink = cut.FindAll("a").FirstOrDefault(a => a.TextContent.Contains("Back"));
        backLink.Should().NotBeNull();

        // Verify GameCard is rendered with correct data
        var gameCard = cut.FindComponent<GameCard>();
        gameCard.Instance.GameId.Should().Be(gameId);
        gameCard.Instance.Play.Should().NotBeNull();
        gameCard.Instance.Play.HomeTeam.Should().Be("Chiefs");
        gameCard.Instance.Play.AwayTeam.Should().Be("Bills");
        gameCard.Instance.Play.HomeScore.Should().Be(21);
        gameCard.Instance.Play.AwayScore.Should().Be(17);
        gameCard.Instance.Play.Quarter.Should().Be(3);
        gameCard.Instance.Play.QuarterSecondsRemaining.Should().Be(600);

        // Verify Stats section is rendered
        cut.Markup.Should().Contain("Stats");
        var statsTables = cut.FindComponents<StatsTable>();
        statsTables.Count.Should().Be(2);

        // Verify home team stats
        var homeStatsTable = statsTables.FirstOrDefault(st => st.Instance.Team == "Chiefs");
        homeStatsTable.Should().NotBeNull();
        homeStatsTable!.Instance.TeamStats.Should().NotBeNull();
        homeStatsTable.Instance.TeamStats!.PassingYards.Should().Be(250);
        homeStatsTable.Instance.TeamStats.Sacks.Should().Be(2);

        // Verify away team stats
        var awayStatsTable = statsTables.FirstOrDefault(st => st.Instance.Team == "Bills");
        awayStatsTable.Should().NotBeNull();
        awayStatsTable!.Instance.TeamStats.Should().NotBeNull();
        awayStatsTable.Instance.TeamStats!.PassingYards.Should().Be(200);
        awayStatsTable.Instance.TeamStats.Sacks.Should().Be(1);

        // Verify no error message is displayed
        cut.FindAll("div").Should().NotContain(e => e.TextContent.Contains("An error ocurred"));

        // Verify Hub was initialized
        mockHubFactory.Verify(x => x.CreateHub(), Times.Once);
        mockHub.Verify(x => x.On("ReceivePlay", It.IsAny<Action<PlayDto>>()), Times.Once);
        mockHub.Verify(x => x.StartAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void GameCast_NullGameReturned_DisplaysErrorMessage()
    {
        // Arrange
        var gameId = 999;

        mockHttp.When($"*/games/{gameId}")
            .Respond("application/json", "null");

        // Act
        var cut = RenderComponent<GameCastPage>(parameters => parameters
            .Add(p => p.GameId, gameId));

        // Assert
        cut.WaitForState(() => cut.Markup.Contains("An error ocurred"), TimeSpan.FromSeconds(2));

        var errorElements = cut.FindAll("div").Where(e => e.TextContent.Contains("An error ocurred"));
        errorElements.Should().NotBeEmpty();
        errorElements.First().TextContent.Should().Contain("An error ocurred. The game could not be loaded.");

        // Verify no GameCard is rendered
        var gameCards = cut.FindComponents<GameCard>();
        gameCards.Count.Should().Be(0);

        // Verify no StatsTable is rendered
        var statsTables = cut.FindComponents<StatsTable>();
        statsTables.Count.Should().Be(0);

        // Verify Hub was NOT initialized due to error
        mockHubFactory.Verify(x => x.CreateHub(), Times.Never);
        mockHub.Verify(x => x.On("ReceivePlay", It.IsAny<Action<PlayDto>>()), Times.Never);
        mockHub.Verify(x => x.StartAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public void GameCast_ExceptionThrownDuringInitialization_DisplaysErrorMessage()
    {
        // Arrange
        var gameId = 1;

        mockHttp.When($"*/games/{gameId}")
            .Throw(new HttpRequestException("Network error"));

        // Act
        var cut = RenderComponent<GameCastPage>(parameters => parameters
            .Add(p => p.GameId, gameId));

        // Assert
        cut.WaitForState(() => cut.Markup.Contains("An error ocurred"), TimeSpan.FromSeconds(2));

        var errorElements = cut.FindAll("div").Where(e => e.TextContent.Contains("An error ocurred"));
        errorElements.Should().NotBeEmpty();
        errorElements.First().TextContent.Should().Contain("An error ocurred. The game could not be loaded.");

        // Verify no GameCard is rendered
        var gameCards = cut.FindComponents<GameCard>();
        gameCards.Count.Should().Be(0);

        // Verify no StatsTable is rendered
        var statsTables = cut.FindComponents<StatsTable>();
        statsTables.Count.Should().Be(0);

        // Verify Hub was NOT initialized due to error
        mockHubFactory.Verify(x => x.CreateHub(), Times.Never);
        mockHub.Verify(x => x.On("ReceivePlay", It.IsAny<Action<PlayDto>>()), Times.Never);
        mockHub.Verify(x => x.StartAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public void GameCast_ApiReturnsError_DisplaysErrorMessage()
    {
        // Arrange
        var gameId = 1;

        mockHttp.When($"*/games/{gameId}")
            .Respond(HttpStatusCode.InternalServerError);

        // Act
        var cut = RenderComponent<GameCastPage>(parameters => parameters
            .Add(p => p.GameId, gameId));

        // Assert
        cut.WaitForState(() => cut.Markup.Contains("An error ocurred"), TimeSpan.FromSeconds(2));

        var errorElements = cut.FindAll("div").Where(e => e.TextContent.Contains("An error ocurred"));
        errorElements.Should().NotBeEmpty();

        // Verify no GameCard is rendered
        var gameCards = cut.FindComponents<GameCard>();
        gameCards.Count.Should().Be(0);

        // Verify Hub was NOT initialized due to error
        mockHubFactory.Verify(x => x.CreateHub(), Times.Never);
    }

    [Fact]
    public void GameCast_HubReceivesUpdate_UpdatesGameAndStats()
    {
        // Arrange
        var gameId = 1;
        Action<PlayDto>? capturedAction = null;

        mockHub.Setup(x => x.On("ReceivePlay", It.IsAny<Action<PlayDto>>()))
            .Callback<string, Action<PlayDto>>((eventName, handler) => capturedAction = handler)
            .Returns(Mock.Of<IDisposable>());

        var game = new GameDto
        {
            Id = gameId,
            Week = 1,
            HomeTeam = "Chiefs",
            HomeScore = 21,
            AwayTeam = "Bills",
            AwayScore = 17,
            State = "InProgress",
            Quarter = 3,
            QuarterSecondsRemaining = 600
        };

        var initialStats = new GameStatDto
        {
            GameId = gameId,
            Stats = new List<StatDto>
            {
                new StatDto { Team = "Chiefs", Home = true, Score = 21, PassingYards = 250 },
                new StatDto { Team = "Bills", Home = false, Score = 17, PassingYards = 200 }
            }
        };

        var updatedStats = new GameStatDto
        {
            GameId = gameId,
            Stats = new List<StatDto>
            {
                new StatDto { Team = "Chiefs", Home = true, Score = 24, PassingYards = 280 },
                new StatDto { Team = "Bills", Home = false, Score = 17, PassingYards = 200 }
            }
        };

        mockHttp.When($"*/games/{gameId}")
            .Respond("application/json", System.Text.Json.JsonSerializer.Serialize(game));

        // First call returns initial stats, subsequent calls return updated stats
        var statsCallCount = 0;
        mockHttp.When($"*/games/{gameId}/stats")
            .Respond(() =>
            {
                statsCallCount++;
                var stats = statsCallCount == 1 ? initialStats : updatedStats;
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(stats),
                        System.Text.Encoding.UTF8,
                        "application/json")
                });
            });

        // Act
        var cut = RenderComponent<GameCastPage>(parameters => parameters
            .Add(p => p.GameId, gameId));

        cut.WaitForState(() => cut.FindComponents<GameCard>().Count == 1, TimeSpan.FromSeconds(2));

        // Simulate receiving a play update from the hub
        capturedAction.Should().NotBeNull();
        var updatedPlay = new PlayDto
        {
            Id = 1,
            GameId = gameId,
            HomeTeam = "Chiefs",
            HomeScore = 24,
            AwayTeam = "Bills",
            AwayScore = 17,
            Quarter = 4,
            QuarterSecondsRemaining = 900,
            Description = "Field Goal Chiefs!"
        };

        cut.InvokeAsync(() => capturedAction!.Invoke(updatedPlay));

        Thread.Sleep(1000); // Allow time for UI to update

        // Assert
        var gameCard = cut.FindComponent<GameCard>();
        gameCard.Instance.Play.HomeScore.Should().Be(24);
        gameCard.Instance.Play.AwayScore.Should().Be(17);
        gameCard.Instance.Play.Quarter.Should().Be(4);
        gameCard.Instance.Play.QuarterSecondsRemaining.Should().Be(900);
        gameCard.Instance.Play.Description.Should().Be("Field Goal Chiefs!");

        // Verify stats were updated
        var statsTables = cut.FindComponents<StatsTable>();
        var homeStatsTable = statsTables.FirstOrDefault(st => st.Instance.Team == "Chiefs");
        homeStatsTable.Should().NotBeNull();
        homeStatsTable!.Instance.TeamStats.Should().NotBeNull();
        homeStatsTable.Instance.TeamStats!.PassingYards.Should().Be(280);
    }

    [Fact]
    public void GameCast_InitialLoad_DisplaysLoadingState()
    {
        // Arrange
        var gameId = 1;

        var game = new GameDto
        {
            Id = gameId,
            Week = 1,
            HomeTeam = "Chiefs",
            HomeScore = 0,
            AwayTeam = "Bills",
            AwayScore = 0,
            State = "InProgress",
            Quarter = 1,
            QuarterSecondsRemaining = 900
        };

        var gameStats = new GameStatDto
        {
            GameId = gameId,
            Stats = new List<StatDto>
            {
                new StatDto { Team = "Chiefs", Home = true, Score = 0 },
                new StatDto { Team = "Bills", Home = false, Score = 0 }
            }
        };

        var responseDelay = TimeSpan.FromMilliseconds(50);
        mockHttp.When($"*/games/{gameId}")
            .Respond(async () =>
            {
                await Task.Delay(responseDelay);
                var content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(game),
                    System.Text.Encoding.UTF8,
                    "application/json");
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = content };
            });

        mockHttp.When($"*/games/{gameId}/stats")
            .Respond("application/json", System.Text.Json.JsonSerializer.Serialize(gameStats));

        // Act
        var cut = RenderComponent<GameCastPage>(parameters => parameters
            .Add(p => p.GameId, gameId));

        // Assert - component should render successfully
        cut.Should().NotBeNull();

        // Initially should show Loading text
        bool hasInitialLoading = cut.Markup.Contains("Loading");

        // Wait for async loading to complete
        cut.WaitForState(() => cut.FindComponents<GameCard>().Count > 0, TimeSpan.FromSeconds(2));

        // Verify Loading state is no longer displayed
        bool hasLoading = cut.Markup.Contains("Loading");
        hasLoading.Should().BeFalse();
    }
}
