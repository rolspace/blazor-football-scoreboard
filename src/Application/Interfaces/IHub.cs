namespace Football.Application.Interfaces;

public interface IHub
{
    Task StartAsync(CancellationToken cancellationToken = default);

    Task SendAsync(string methodName, object arg1, CancellationToken cancellationToken = default);

    Task StopAsync(CancellationToken cancellationToken = default);

    IDisposable On<T>(string methodName, Action<T> handler);

    ValueTask DisposeAsync();
}
