namespace Football.Worker.Extensions;

public static class HostEnvironmentExtensions
{
    public static bool IsLocalhost(this IHostEnvironment hostEnvironment)
    {
        return hostEnvironment.EnvironmentName == "Localhost";
    }
}
