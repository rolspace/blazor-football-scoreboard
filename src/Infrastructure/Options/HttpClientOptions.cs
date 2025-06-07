namespace Football.Infrastructure.Options;

public class HttpClientOptions
{
    public const string Key = "HttpClient";

    public string BaseAddress { get; set; } = string.Empty;

    public RetryOptions RetryOptions { get; set; } = new RetryOptions();
}
