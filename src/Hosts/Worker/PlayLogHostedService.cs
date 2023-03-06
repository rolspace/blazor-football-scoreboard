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
            await _hubProvider.StartAsync();
            _logger.LogInformation("Service started");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred starting the SignalR connection hub");
            throw ex;
        }
    }

    private async void DoWork(object? state)
    {
        var gameTime = state as GameTime;
        if (gameTime == null) throw new InvalidOperationException("Worker state cannot be null");

        int previousTime = gameTime.Counter;

        Interlocked.Decrement(ref gameTime.Counter);
        int currentTime = gameTime.Counter;

        Console.WriteLine(currentTime);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _hubProvider.StopAsync();

        _logger.LogInformation("Service stopped");
    }

    public async ValueTask DisposeAsync()
    {
        await _hubProvider.DisposeAsync();

        _logger.LogInformation("Service disposed");
    }
}
