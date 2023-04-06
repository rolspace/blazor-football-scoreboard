using Football.Application;
using Football.Application.Interfaces;
using Football.Application.Models;
using Football.Application.Plays.Queries;
using Football.Application.Stats.Commands;
using MediatR;

// TODO: Convert to background service
namespace Football.Worker;

public class PlayLogHostedService : IHostedService, IAsyncDisposable
{
    private readonly IHubManager _hubManager;

    private readonly IServiceScopeFactory _scopeFactory;

    private readonly ILogger<PlayLogHostedService> _logger;

    private Timer? _gameTimer;

    public PlayLogHostedService(IHubManager hubManager, IServiceScopeFactory scopeFactory, ILogger<PlayLogHostedService> logger)
    {
        _hubManager = hubManager;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var gameTime = new GameTimeManager();
        _gameTimer = new Timer(new TimerCallback(DoWork), gameTime, TimeSpan.Zero, TimeSpan.FromSeconds(1));

        try
        {
            await _hubManager.StartAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred starting the SignalR connection hub");
        }
        finally
        {
            _logger.LogInformation("Hosted service started");
        }
    }

    private async void DoWork(object? state)
    {
        var gameTimeManager = state as GameTimeManager;
        if (gameTimeManager == null) throw new InvalidOperationException("Worker state cannot be null");

        try
        {
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

                int quarter = gameTimeManager.GetQuarter();
                int quarterSecondsRemaining = gameTimeManager.GetQuarterSecondsRemaining();
                var query = new GetPlaysQuery()
                {
                    Week = 1,
                    Quarter = quarter,
                    QuarterSecondsRemaining = quarterSecondsRemaining
                };

                IEnumerable<PlayDto> playDtos = await mediator.Send(query);

                int gameOverCount = playDtos.Count(p => p.GameOver);
                if (gameOverCount > 0) gameTimeManager.IncrementGamesOver(gameOverCount);

                foreach (PlayDto playDto in playDtos)
                {
                    var saveStatsCommand = new SaveStatsCommand()
                    {
                        GameId = playDto.GameId,
                        Quarter = playDto.Quarter,
                        QuarterSecondsRemaining = playDto.QuarterSecondsRemaining,
                        GameOver = playDto.GameOver,
                        SaveStatCommandItems = new List<SaveStatsCommandItem>()
                        {
                            new SaveStatsCommandItem() {
                                Team = playDto.HomeTeam,
                                Score = playDto.HomeScore,
                                YardsGained = playDto.HomeTeamOnOffense ? playDto.YardsGained : 0,
                                Sacks = playDto.AwayTeamOnOffense && Convert.ToBoolean(playDto.Sack) ? 1 : 0,
                                ReturnYards = playDto.Kickoff && playDto.HomeTeamPossession && playDto.ReturnYards != null ? (int)playDto.ReturnYards : 0,
                                Punts = playDto.Punt && playDto.HomeTeamPossession && Convert.ToBoolean(playDto.PuntAttempt) ? 1 : 0
                            },
                            new SaveStatsCommandItem() {
                                Team = playDto.AwayTeam,
                                Score = playDto.AwayScore,
                                YardsGained = playDto.AwayTeamOnOffense ? playDto.YardsGained : 0,
                                Sacks = playDto.HomeTeamOnOffense && Convert.ToBoolean(playDto.Sack) ? 1 : 0,
                                ReturnYards = playDto.Kickoff && playDto.HomeTeamPossession && playDto.ReturnYards != null ? (int)playDto.ReturnYards : 0,
                                Punts = playDto.Punt && playDto.HomeTeamPossession && Convert.ToBoolean(playDto.PuntAttempt) ? 1 : 0
                            }
                        }
                    };

                    await mediator.Send(saveStatsCommand);

                    _logger.LogInformation($"{quarter}/{quarterSecondsRemaining} - {playDto.ToString()}");
                }

                gameTimeManager.SetTime();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error ocurred trying to retrieve play log data.");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _hubManager.StopAsync();

        _logger.LogInformation("Service stopped");
    }

    public async ValueTask DisposeAsync()
    {
        if (_gameTimer != null) await _gameTimer.DisposeAsync();

        await _hubManager.DisposeAsync();

        _logger.LogInformation("Service disposed");
    }
}
