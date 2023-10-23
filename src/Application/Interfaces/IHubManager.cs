namespace Football.Application.Interfaces;

public interface IHubManager
{
    Task StartAsync(CancellationToken cancellationToken);

    Task StopAsync(CancellationToken cancellationToken);

    Task SendAsync<T>(string methodName, T? arg1, CancellationToken cancellationToken);

    IDisposable On<T>(string methodName, Action<T> handler);

    Task DisposeAsync();
}
