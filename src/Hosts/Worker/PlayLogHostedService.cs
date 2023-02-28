namespace Football.Worker;

public class PlayLogHostedService : IHostedService, IAsyncDisposable
{
    private readonly ILogger<PlayLogHostedService> _logger;

    public PlayLogHostedService(ILogger<PlayLogHostedService> logger)
    {
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service started");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service stopped");
    }

    public async ValueTask DisposeAsync()
    {
        _logger.LogInformation("Service disposed");
    }
}
