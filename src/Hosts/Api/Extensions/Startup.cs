namespace Football.Api.Extensions;

public static class Startup
{
    public static bool IsLocalhost(this IWebHostEnvironment webHostEnvironment)
    {
        return webHostEnvironment.EnvironmentName == "Localhost";
    }
}
