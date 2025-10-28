using Football.Application.Interfaces;
using Football.Infrastructure.Options;
using Football.Infrastructure.Services;
using Microsoft.Extensions.Options;

namespace Football.Infrastructure.Factories;

public class HubConnectionFactory : IHubConnectionFactory<IHub>
{
    private readonly Uri _hubUri;

    public HubConnectionFactory(IOptions<HubOptions> hubOptionsAccesor)
    {
        _hubUri = new Uri(hubOptionsAccesor.Value.HubUrl);
    }

    public IHub CreateHub()
    {
        return new Hub(_hubUri);
    }
}
