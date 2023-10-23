using Football.Application.Features.Plays.Models;
using Football.Application.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace Football.Infrastructure.Hub;

public class HubManager : IHubManager
{
    private readonly HubConnection _hubConnection;

    public HubManager(IOptions<HubSettings> hubSettings)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(hubSettings.Value.HubUrl)
            .Build();
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

    public IDisposable On(string methodName, Action<PlayDto> handler)
    {
        return _hubConnection.On(methodName, handler);
    }

    public async Task DisposeAsync()
    {
        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.DisposeAsync();
        }
    }
}
