using Football.Application.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace Football.Infrastructure.Hub;

public class HubManager : IHubManager
{
    private readonly HubConnection _hubConnection;

    public HubManager(Uri hubUri)
    {
        _hubConnection = new HubConnectionBuilder().WithUrl(hubUri).Build();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync(cancellationToken);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            await _hubConnection.StopAsync(cancellationToken);
        }
    }

    public async Task SendAsync<T>(string methodName, T? arg1, CancellationToken cancellationToken)
    {
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            await _hubConnection.SendAsync(methodName, arg1, cancellationToken);
        }
    }

    public async Task DisposeAsync()
    {
        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.DisposeAsync();
        }
    }
}
