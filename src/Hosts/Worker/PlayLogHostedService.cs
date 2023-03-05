using Football.Worker.Providers;

namespace Football.Worker;

public class PlayLogHostedService : IHostedService, IAsyncDisposable
{
    private readonly IHubProvider _hubProvider;

    private readonly ILogger<PlayLogHostedService> _logger;

    private readonly IServiceScopeFactory _scopeFactory;

    public PlayLogHostedService(IHubProvider hubProvider, IServiceScopeFactory scopeFactory, ILogger<PlayLogHostedService> logger)
    {
        _hubProvider = hubProvider;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service started");

        try
        {
            await _hubProvider.StartHubAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred starting the SignalR connection hub");
        }
    }

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
