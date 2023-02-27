namespace Football.Worker;

public class StreamService : IHostedService, IAsyncDisposable
{
    private readonly ILogger<StreamService> _logger;

    public StreamService(ILogger<StreamService> logger)
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
