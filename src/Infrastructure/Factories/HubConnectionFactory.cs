using System;
using Football.Application.Interfaces;
using Football.Infrastructure.Options;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace Football.Infrastructure.Factories;

public class HubConnectionFactory : IHubConnectionFactory<HubConnection>
{
    private readonly Uri _hubUri;

    public HubConnectionFactory(IOptions<HubOptions> hubOptionsAccesor)
    {
        _hubUri = new Uri(hubOptionsAccesor.Value.HubUrl);
    }

    public HubConnection CreateHubConnection()
    {
        return new HubConnectionBuilder().WithUrl(_hubUri).Build();
    }
}
