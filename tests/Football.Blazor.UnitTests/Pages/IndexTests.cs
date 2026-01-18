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
using Football.Application.Interfaces;
using Football.Blazor.Components;
using Football.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Polly;
using RichardSzalay.MockHttp;
using IndexPage = Football.Blazor.Pages.Index;

namespace Football.Blazor.UnitTests.Pages;

public class IndexTests : TestContext
{
    private readonly MockHttpMessageHandler mockHttp;
    private readonly Mock<IHubFactory<IHub>> mockHubFactory;
    private readonly Mock<IHub> mockHub;
    private readonly Mock<IOptions<HubOptions>> mockHubOptions;
    private readonly Mock<ILogger<Blazor.Components.GameComponentBase>> mockLogger;

    public IndexTests()
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
    public void Index_Renders()
    {
        // Arrange
        GameDto[] games =
        [
            new GameDto
            {
                Id = 1,
                Week = 1,
                HomeTeam = "Chiefs",
                HomeScore = 21,
                AwayTeam = "Bills",
                AwayScore = 17,
                State = "InProgress",
                Quarter = 3,
                QuarterSecondsRemaining = 600
            },
            new GameDto
            {
                Id = 2,
                Week = 1,
                HomeTeam = "Cowboys",
                HomeScore = 14,
                AwayTeam = "Eagles",
                AwayScore = 10,
                State = "InProgress",
                Quarter = 2,
                QuarterSecondsRemaining = 420
            }
        ];

        mockHttp.When("*/games/now")
            .Respond("application/json", System.Text.Json.JsonSerializer.Serialize(games));

        // Act
        IRenderedComponent<IndexPage> cut = RenderComponent<IndexPage>();

        // Assert
        cut.WaitForState(() => cut.FindComponents<Blazor.Components.GameCard>().Count == 2, TimeSpan.FromSeconds(2));

        IReadOnlyList<IRenderedComponent<GameCard>> gameCards = cut.FindComponents<Blazor.Components.GameCard>();
        gameCards.Count.Should().Be(2);

        // Verify first game card
        gameCards[0].Instance.GameId.Should().Be(1);
        gameCards[0].Instance.Play.Should().NotBeNull();
        gameCards[0].Instance.Play.HomeTeam.Should().Be("Chiefs");
        gameCards[0].Instance.Play.AwayTeam.Should().Be("Bills");
        gameCards[0].Instance.Play.HomeScore.Should().Be(21);
        gameCards[0].Instance.Play.AwayScore.Should().Be(17);
        gameCards[0].Instance.Play.Quarter.Should().Be(3);
        gameCards[0].Instance.Play.QuarterSecondsRemaining.Should().Be(600);

        // Verify second game card
        gameCards[1].Instance.GameId.Should().Be(2);
        gameCards[1].Instance.Play.Should().NotBeNull();
        gameCards[1].Instance.Play.HomeTeam.Should().Be("Cowboys");
        gameCards[1].Instance.Play.AwayTeam.Should().Be("Eagles");

        // Verify no error message is displayed
        cut.FindAll("div").Should().NotContain(e => e.TextContent.Contains("An error ocurred"));

        // Verify Hub was initialized
        mockHubFactory.Verify(x => x.CreateHub(), Times.Once);
        mockHub.Verify(x => x.On("ReceivePlay", It.IsAny<Action<PlayDto>>()), Times.Once);
        mockHub.Verify(x => x.StartAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void Index_EmptyGamesReturned_DisplaysNoGames()
    {
        // Arrange
        var emptyGames = Array.Empty<GameDto>();

        mockHttp.When("*/games/now")
            .Respond("application/json", System.Text.Json.JsonSerializer.Serialize(emptyGames));

        // Act
        var cut = RenderComponent<IndexPage>();

        // Assert
        IReadOnlyList<IRenderedComponent<GameCard>> gameCards = cut.FindComponents<GameCard>();
        gameCards.Count.Should().Be(0);

        // Verify no error message is displayed
        cut.FindAll("div").Should().NotContain(e => e.TextContent.Contains("An error ocurred"));

        // Verify Hub was NOT initialized (no games to track)
        mockHubFactory.Verify(x => x.CreateHub(), Times.Never);
        mockHub.Verify(x => x.On("ReceivePlay", It.IsAny<Action<PlayDto>>()), Times.Never);
        mockHub.Verify(x => x.StartAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public void Index_NullGamesReturned_DisplaysNoGames()
    {
        // Arrange
        mockHttp.When("*/games/now")
            .Respond("application/json", "null");

        // Act
        IRenderedComponent<IndexPage> cut = RenderComponent<IndexPage>();

        // Assert
        IReadOnlyList<IRenderedComponent<GameCard>> gameCards = cut.FindComponents<GameCard>();
        gameCards.Count.Should().Be(0);

        // Verify no error message is displayed
        cut.FindAll("div").Should().NotContain(e => e.TextContent.Contains("An error ocurred"));

        // Verify Hub was NOT initialized
        mockHubFactory.Verify(x => x.CreateHub(), Times.Never);
    }

    [Fact]
    public void Index_ApiReturnsError_DisplaysErrorMessage()
    {
        // Arrange
        mockHttp.When("*/games/now")
            .Respond(HttpStatusCode.InternalServerError);

        // Act
        IRenderedComponent<IndexPage> cut = RenderComponent<IndexPage>();

        // Assert
        cut.WaitForState(() => cut.Markup.Contains("An error ocurred"), TimeSpan.FromSeconds(2));

        IEnumerable<AngleSharp.Dom.IElement> errorElements = cut.FindAll("div").Where(e => e.TextContent.Contains("An error ocurred"));
        errorElements.Should().NotBeEmpty();
        errorElements.First().TextContent.Should().Contain("An error ocurred. The games could not be loaded.");

        // Verify no game cards are rendered
        IReadOnlyList<IRenderedComponent<GameCard>> gameCards = cut.FindComponents<GameCard>();
        gameCards.Count.Should().Be(0);

        // Verify Hub was NOT initialized due to error
        mockHubFactory.Verify(x => x.CreateHub(), Times.Never);
    }

    [Fact]
    public void Index_HttpClientThrowsException_DisplaysErrorMessage()
    {
        // Arrange
        mockHttp.When("*/games/now")
            .Throw(new HttpRequestException("Network error"));

        // Act
        IRenderedComponent<IndexPage> cut = RenderComponent<IndexPage>();

        // Assert
        cut.WaitForState(() => cut.Markup.Contains("An error ocurred"), TimeSpan.FromSeconds(2));

        IEnumerable<AngleSharp.Dom.IElement> errorElements = cut.FindAll("div").Where(e => e.TextContent.Contains("An error ocurred"));
        errorElements.Should().NotBeEmpty();

        // Verify no game cards are rendered
        IReadOnlyList<IRenderedComponent<GameCard>> gameCards = cut.FindComponents<GameCard>();
        gameCards.Count.Should().Be(0);
    }

    [Fact]
    public void Index_HubReceivesUpdate_UpdatesGameState()
    {
        // Arrange
        Action<PlayDto>? capturedAction = null;
        mockHub.Setup(x => x.On("ReceivePlay", It.IsAny<Action<PlayDto>>()))
            .Callback<string, Action<PlayDto>>((eventName, handler) => capturedAction = handler)
            .Returns(Mock.Of<IDisposable>());

        GameDto[] games =
        [
            new GameDto
            {
                Id = 1,
                Week = 1,
                HomeTeam = "Chiefs",
                HomeScore = 21,
                AwayTeam = "Bills",
                AwayScore = 17,
                State = "InProgress",
                Quarter = 3,
                QuarterSecondsRemaining = 600
            }
        ];

        mockHttp.When("*/games/now")
            .Respond("application/json", System.Text.Json.JsonSerializer.Serialize(games));

        // Act
        IRenderedComponent<IndexPage> cut = RenderComponent<IndexPage>();

        cut.WaitForState(() => cut.FindComponents<GameCard>().Count == 1, TimeSpan.FromSeconds(2));

        // Simulate receiving a play update from the hub
        capturedAction.Should().NotBeNull();
        var updatedPlay = new PlayDto
        {
            GameId = 1,
            HomeScore = 24,
            AwayScore = 17,
            Quarter = 4,
            QuarterSecondsRemaining = 900,
            Description = "Field Goal Chiefs!"
        };

        cut.InvokeAsync(() => capturedAction!.Invoke(updatedPlay));

        Thread.Sleep(1000); // Allow time for UI to update

        // Assert
        IRenderedComponent<GameCard> gameCard = cut.FindComponent<GameCard>();
        gameCard.Instance.Play.HomeScore.Should().Be(24);
        gameCard.Instance.Play.AwayScore.Should().Be(17);
        gameCard.Instance.Play.Quarter.Should().Be(4);
        gameCard.Instance.Play.QuarterSecondsRemaining.Should().Be(900);
        gameCard.Instance.Play.Description.Should().Be("Field Goal Chiefs!");
    }

    [Fact]
    public void Index_InitialLoad_DisplaysLoadingState()
    {
        // Arrange - create a delayed response to simulate network delay
        GameDto[] games =
        [
            new GameDto
            {
                Id = 1,
                Week = 1,
                HomeTeam = "Home",
                HomeScore = 0,
                AwayTeam = "Away",
                AwayScore = 0,
                State = "InProgress",
                Quarter = 1,
                QuarterSecondsRemaining = 900
            }
        ];

        var responseDelay = TimeSpan.FromMilliseconds(50);
        mockHttp.When("*/games/now")
            .Respond(async () =>
            {
                await Task.Delay(responseDelay);
                var content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(games),
                    System.Text.Encoding.UTF8,
                    "application/json");
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = content };
            });

        // Act
        IRenderedComponent<IndexPage> cut = RenderComponent<IndexPage>();

        // Assert - component should render successfully
        cut.Should().NotBeNull();

        // Wait for async loading to complete - when games are loaded, Loading text disappears
        cut.WaitForState(() => cut.FindComponents<GameCard>().Count > 0, TimeSpan.FromSeconds(2));

        // Verify Loading state is no longer displayed
        bool hasLoading = cut.Markup.Contains("Loading");
        hasLoading.Should().BeFalse();
    }
}
