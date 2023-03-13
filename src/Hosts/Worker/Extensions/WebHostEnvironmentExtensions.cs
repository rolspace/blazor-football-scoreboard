namespace Football.Worker.Extensions;

public static class WebHostEnvironmentExtensions
{
    public static bool IsLocalhost(this IWebHostEnvironment webHostEnvironment)
    {
        return webHostEnvironment.EnvironmentName == "Localhost";
    }
}
