namespace Football.Infrastructure.Options;

public class HubOptions
{
    public const string Key = "Hub";

    public string HubUrl { get; set; } = string.Empty;

    public RetryOptions RetryOptions { get; set; } = new RetryOptions();
}
