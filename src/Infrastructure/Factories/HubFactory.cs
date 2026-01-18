using Football.Application.Interfaces;
using Football.Infrastructure.Options;
using Football.Infrastructure.Services;
using Microsoft.Extensions.Options;
using HubException = Football.Infrastructure.Exceptions.HubException;

namespace Football.Infrastructure.Factories;

public class HubFactory(IOptions<HubOptions> hubOptionsAccesor) : IHubFactory<IHub>
{
    private readonly string _hubUrl = hubOptionsAccesor?.Value?.HubUrl ?? string.Empty;

    public IHub CreateHub()
    {
        if (string.IsNullOrEmpty(_hubUrl))
        {
            throw new HubException("Hub URL is not configured.");
        }

        try
        {
            Uri hubUri = new(_hubUrl);

            return new Hub(hubUri);
        }
        catch (Exception ex)
        {
            throw new HubException("Failed to create Hub connection.", ex);
        }
    }
}
