using Football.Application.Features.Plays.Models;
using Football.Infrastructure.Options;
using Microsoft.AspNetCore.SignalR.Client;
using Polly;
using Polly.Retry;
using Polly.Timeout;

namespace Football.Infrastructure.Extensions
{
    public static class HubConnectionExtensions
    {
        public static ResiliencePipeline GetHubConnectionPipeline(HubOptions hubOptions)
        {
            ArgumentNullException.ThrowIfNull(hubOptions);

            return new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions {
                    MaxRetryAttempts = hubOptions.RetryOptions.MaxRetryAttempts,
                    BackoffType = hubOptions.RetryOptions.BackoffType
                })
                .AddTimeout(new TimeoutStrategyOptions())
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
