using Microsoft.Extensions.Hosting;

namespace Football.Infrastructure.Extensions;

public static class HostEnvironmentExtensions
{
    public static bool IsLocalhost(this IHostEnvironment hostEnvironment)
    {
        return hostEnvironment.EnvironmentName == "Localhost";
    }
}
