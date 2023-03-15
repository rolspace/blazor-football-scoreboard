using Football.Application.Common.Models;
using Football.Application.Queries.Plays;
using Football.Worker.Providers;
using MediatR;

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
        var gameTime = new GameTime();
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
        var gameTime = state as GameTime;
        if (gameTime == null) throw new InvalidOperationException("Worker state cannot be null");

        int quarter = gameTime.GetQuarter();
        int quarterSecondsRemaining = gameTime.GetQuarterSecondsRemaining();

        try
        {
            GetPlaysQuery query = new GetPlaysQuery()
            {
                Week = 1,
                Quarter = quarter,
                QuarterSecondsRemaining = quarterSecondsRemaining
            };

            using (var scope = _scopeFactory.CreateScope())
            {
                ISender mediator = scope.ServiceProvider.GetRequiredService<ISender>();
                var plays = await mediator.Send(query);

                foreach (PlayDto play in plays)
                {
                    _logger.LogInformation($"{quarter}/{quarterSecondsRemaining} - {play.ToString()}");
                }
            }

            gameTime.DecreaseSecondsRemaining();
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
