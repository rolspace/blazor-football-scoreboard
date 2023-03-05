using Football.Worker.Providers;

namespace Football.Worker;

public class PlayLogHostedService : IHostedService, IAsyncDisposable
{
    private readonly IHubProvider _hubProvider;

    private readonly ILogger<PlayLogHostedService> _logger;

    private readonly IServiceScopeFactory _scopeFactory;

    private Timer? _gameTimer;

    internal class GameTime
    {
        public int Counter = 3600;
    }

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
            await _hubProvider.StartHubAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred starting the SignalR connection hub");
        }
        finally
        {
            _logger.LogInformation("Service started");
        }
    }

    private async void DoWork(object? state)
    { }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service stopped");

        await _hubProvider.StopHubAsync();
    }

    public async ValueTask DisposeAsync()
    {
        _logger.LogInformation("Service disposed");

        await _hubProvider.DisposeHubAsync();
    }
}
