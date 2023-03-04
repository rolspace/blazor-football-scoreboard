using Microsoft.AspNetCore.SignalR.Client;

namespace Football.Worker.Providers;

public class HubProvider : IHubProvider
{
    private readonly HubConnection _hubConnection;

    public HubProvider(Uri hubUri)
    {
        _hubConnection = new HubConnectionBuilder().WithUrl(hubUri).Build();
    }

    public async Task StartHubAsync()
    {
        await _hubConnection.StartAsync();
    }

    public async Task StopHubAsync()
    {
        await _hubConnection.StopAsync();
    }

    public async Task DisposeHubAsync()
    {
        await _hubConnection.DisposeAsync();
    }
}
