namespace Football.Application.Interfaces;

public interface IHubManager
{
    Task StartAsync(CancellationToken cancellationToken);

    Task StopAsync(CancellationToken cancellationToken);

    Task SendAsync<T>(string methodName, T? arg1, CancellationToken cancellationToken);

    Task DisposeAsync();
}
