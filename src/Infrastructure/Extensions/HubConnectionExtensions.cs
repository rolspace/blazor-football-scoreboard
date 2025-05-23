using Football.Application.Features.Plays.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Polly;
using Polly.Retry;
using Polly.Timeout;

namespace Football.Infrastructure.Extensions
{
    public static class HubConnectionExtensions
    {
        private static readonly ResiliencePipeline s_resiliencePipeline;

        static HubConnectionExtensions()
        {
            s_resiliencePipeline = new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions())
                .AddTimeout(new TimeoutStrategyOptions())
                .Build();
        }

        public static async Task StartWithRetryAsync(this HubConnection hubConnection, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(hubConnection);

            await s_resiliencePipeline.ExecuteAsync(async (token) =>
            {
                await hubConnection.StartAsync(token);
            }, cancellationToken);
        }

        public static async Task SendPlayWithRetryAsync(this HubConnection hubConnection, PlayDto playDto, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(hubConnection);

            ArgumentNullException.ThrowIfNull(playDto);

            await s_resiliencePipeline.ExecuteAsync(async (token) =>
            {
                await hubConnection.SendAsync("SendPlay", playDto, token);
            }, cancellationToken);
        }
    }
}
