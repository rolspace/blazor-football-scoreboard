namespace Football.Api.Extensions;

public static class HostEnvironmentExtensions
{
    public static bool IsLocalhost(this IHostEnvironment hostEnvironment)
    {
        return hostEnvironment.EnvironmentName == "Localhost";
    }
}
