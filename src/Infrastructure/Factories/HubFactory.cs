using Football.Application.Interfaces;
using Football.Infrastructure.Options;
using Football.Infrastructure.Services;
using Microsoft.Extensions.Options;

namespace Football.Infrastructure.Factories;

public class HubFactory(IOptions<HubOptions> hubOptionsAccesor) : IHubFactory<IHub>
{
    private readonly string _hubUrl = hubOptionsAccesor?.Value?.HubUrl ?? string.Empty;

    public IHub CreateHub()
    {
        if (string.IsNullOrEmpty(_hubUrl))
        {
            throw new InvalidOperationException("Hub URL is not configured.");
        }

        Uri hubUri = new(_hubUrl);

        return new Hub(hubUri);
    }
}
