using Football.Application.Features.Plays.Models;
using Football.Infrastructure.Options;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using Polly.Timeout;

namespace Football.Infrastructure.Extensions
{
    public static class HubConnectionExtensions
    {
        public static ResiliencePipeline GetHubConnectionPipeline(HubOptions hubOptions, ILogger logger)
        {
            ArgumentNullException.ThrowIfNull(hubOptions);

            return new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions
                {
                    MaxRetryAttempts = hubOptions.RetryOptions.MaxRetryAttempts,
                    BackoffType = hubOptions.RetryOptions.BackoffType,
                    OnRetry = args =>
                    {
                        logger.LogWarning("Retrying connection to hub. Attempt {AttemptNumber}", args.AttemptNumber);

                        return default;
                    }
                })
                .AddTimeout(new TimeoutStrategyOptions
                {
                    OnTimeout = args =>
                    {
                        logger.LogError("Connection to hub timed out after {TotalSeconds} seconds", args.Timeout.TotalSeconds);
                        return default;
                    },
                })
                .Build();
        }

        public static async Task StartWithRetryAsync(this HubConnection hubConnection,
            ResiliencePipeline pipeline, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(hubConnection);

            await pipeline.ExecuteAsync(async (token) =>
            {
                await hubConnection.StartAsync(token);
            }, cancellationToken);
        }

        public static async Task SendPlayWithRetryAsync(this HubConnection hubConnection,
            ResiliencePipeline pipeline, PlayDto playDto, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(hubConnection);

            ArgumentNullException.ThrowIfNull(playDto);

            await pipeline.ExecuteAsync(async (token) =>
            {
                await hubConnection.SendAsync("SendPlay", playDto, token);
            }, cancellationToken);
        }
    }
}
