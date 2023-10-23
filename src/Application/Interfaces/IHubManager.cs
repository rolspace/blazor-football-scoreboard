using Football.Application.Features.Plays.Models;

namespace Football.Application.Interfaces;

public interface IHubManager
{
    Task StartAsync(CancellationToken cancellationToken);

    Task StopAsync(CancellationToken cancellationToken);

    Task SendAsync<T>(string methodName, T? arg1, CancellationToken cancellationToken);

    IDisposable On(string methodName, Action<PlayDto> handler);

    Task DisposeAsync();
}
