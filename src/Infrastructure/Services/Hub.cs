using Football.Application.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace Football.Infrastructure.Services;

public class Hub(Uri hubUri) : IHub
{
    private readonly HubConnection _hubConnection = new HubConnectionBuilder()
            .WithUrl(hubUri)
            .Build();

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        await _hubConnection.StartAsync(cancellationToken);
    }

    public async Task SendAsync(string methodName, object arg1, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(methodName);
        ArgumentNullException.ThrowIfNull(arg1);

        await _hubConnection.SendAsync(methodName, arg1, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        await _hubConnection.StopAsync(cancellationToken);
    }

    public IDisposable On<T>(string methodName, Action<T> handler)
    {
        ArgumentException.ThrowIfNullOrEmpty(methodName);
        ArgumentNullException.ThrowIfNull(handler);

        return _hubConnection.On(methodName, handler);
    }

    public async Task DisposeAsync()
    {
        await _hubConnection.DisposeAsync();
    }
}
