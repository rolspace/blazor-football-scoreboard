using Football.Application.Common.Models;
using Football.Application.Queries.Plays;
using Football.Worker.Providers;
using MediatR;

// TODO: Convert to background service
namespace Football.Worker;

public class PlayLogHostedService : IHostedService, IAsyncDisposable
{
    private readonly IHubProvider _hubProvider;

    private readonly IServiceScopeFactory _scopeFactory;

    private readonly ILogger<PlayLogHostedService> _logger;

    private Timer? _gameTimer;

    public PlayLogHostedService(IHubProvider hubProvider, IServiceScopeFactory scopeFactory, ILogger<PlayLogHostedService> logger)
    {
        _hubProvider = hubProvider;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var gameTime = new GameTimeManager();
        _gameTimer = new Timer(new TimerCallback(DoWork), gameTime, TimeSpan.Zero, TimeSpan.FromSeconds(1));

        try
        {
            await _hubProvider.StartAsync();
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

                if (gameTimeManager.IsEndOfRegulation)
                {
                    bool startOvertime = await mediator.Send(new CheckOvertimeQuery() { Week = 1 });

                    if (startOvertime)
                    {
                        gameTimeManager.StartOvertime(startOvertime);
                    }
                    else
                    {
                        gameTimeManager.End();
                    }
                }

                int quarter = gameTimeManager.GetQuarter();
                int quarterSecondsRemaining = gameTimeManager.GetQuarterSecondsRemaining();
                var query = new GetPlaysQuery()
                {
                    Week = 1,
                    Quarter = quarter,
                    QuarterSecondsRemaining = quarterSecondsRemaining
                };

                gameTimeManager.PassTime();

                IEnumerable<PlayDto> plays = await mediator.Send(query);

                foreach (PlayDto play in plays)
                {
                    _logger.LogInformation($"{quarter}/{quarterSecondsRemaining} - {play.ToString()}");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error ocurred trying to retrieve play log data.");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _hubProvider.StopAsync();

        _logger.LogInformation("Service stopped");
    }

    public async ValueTask DisposeAsync()
    {
        if (_gameTimer != null) await _gameTimer.DisposeAsync();

        await _hubProvider.DisposeAsync();

        _logger.LogInformation("Service disposed");
    }
}
