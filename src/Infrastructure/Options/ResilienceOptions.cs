using Polly;

namespace Football.Infrastructure.Options;

public class RetryOptions
{
    public int MaxRetryAttempts { get; set; } = 3;

    public DelayBackoffType BackoffType { get; set; } = DelayBackoffType.Constant;
}
