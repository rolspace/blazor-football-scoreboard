namespace Football.Api.Settings;

public class CorsSettings
{
    public const string CorsSection = "Cors";

    public string PolicyName { get; set; } = string.Empty;

    public string AllowedOrigins { get; set; } = string.Empty;

    public string AllowedMethods { get; set; } = string.Empty;
}
