using Football.Application.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace Football.Infrastructure.Communication;

public class HubManager : IHubManager
{
    private readonly HubConnection _hubConnection;

    public HubManager(Uri hubUri)
    {
        _hubConnection = new HubConnectionBuilder().WithUrl(hubUri).Build();
    }

    public async Task StartAsync()
    {
        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }
    }

    public async Task StopAsync()
    {
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            await _hubConnection.StopAsync();
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
