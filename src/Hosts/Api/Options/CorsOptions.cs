namespace Football.Api.Options;

public class CorsOptions
{
    public const string Key = "Cors";

    public string PolicyName { get; set; } = string.Empty;

    public string AllowedOrigins { get; set; } = string.Empty;

    public string AllowedMethods { get; set; } = string.Empty;
}
