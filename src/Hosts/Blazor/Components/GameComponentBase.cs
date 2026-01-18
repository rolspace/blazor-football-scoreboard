using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Football.Application.Features.Games.Models;
using Football.Application.Features.Plays.Models;
using Football.Application.Interfaces;
using Football.Infrastructure.Extensions;
using Football.Infrastructure.Options;
using Polly;
using HubExtensions = Football.Infrastructure.Extensions.HubExtensions;

namespace Football.Blazor.Components;

public class GameComponentBase : ComponentBase
{
    private HubOptions hubOptions => HubOptions.Value;

    [Inject]
    private IOptions<HubOptions> HubOptions { get; set; } = null!;

    [Inject]
    protected IHttpClientFactory HttpClientFactory { get; set; } = null!;

    [Inject]
    protected IHubFactory<IHub> HubConnectionFactory { get; set; } = null!;

    [Inject]
    protected ILogger<GameComponentBase> Logger { get; set; } = null!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = null!;

    protected IHub? hub;

    protected bool errorOcurred = false;

    protected async Task InitializeHubConnection(Action<PlayDto> onPlayReceived)
    {
        try
        {
            hub = HubConnectionFactory.CreateHub();
            hub.On("ReceivePlay", onPlayReceived);

            ResiliencePipeline pipeline = HubExtensions.GetHubPipeline(hubOptions, Logger);
            await hub.StartWithRetryAsync(pipeline, new CancellationToken());
        }
        catch (Exception ex)
        {
            // TODO: Create a custom connection exception
            Logger.LogError(ex, "Error initializing Hub connection.");
            throw;
        }
    }

    protected static PlayDto GetPreviousPlayFromGame(GameDto game)
    {
        return new PlayDto
        {
            Id = 0,
            GameId = game.Id,
            HomeTeam = game.HomeTeam,
            HomeScore = game.HomeScore,
            AwayTeam = game.AwayTeam,
            AwayScore = game.AwayScore,
            Quarter = game.Quarter,
            QuarterSecondsRemaining = game.QuarterSecondsRemaining
        };
    }
}
