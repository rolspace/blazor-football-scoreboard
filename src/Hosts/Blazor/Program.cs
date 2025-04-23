using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using Football.Application.Interfaces;
using Football.Blazor;
using Football.Blazor.Options;
using Football.Infrastructure.Factories;
using Football.Infrastructure.Options;
using Polly;
using Serilog;
using Polly.Retry;
using Polly.Timeout;

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.BrowserConsole()
                .CreateLogger();

try
{
    Log.Information("Blazor host application start.");

    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");

    builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection(ApiOptions.Key));
    builder.Services.Configure<HubOptions>(builder.Configuration.GetSection(HubOptions.Key));

    builder.Services.AddSingleton<IHubConnectionFactory<HubConnection>, HubConnectionFactory>();

    builder.Services.AddHttpClient(Constants.DefaultHttpClient, (serviceProvider, client) =>
    {
        ApiOptions apiOptions = serviceProvider.GetRequiredService<IOptions<ApiOptions>>().Value;
        client.BaseAddress = new Uri(apiOptions.ApiBaseUrl);
    }).AddResilienceHandler(Constants.DefaultPipeline, handlerBuilder =>
    {
        handlerBuilder
            .AddRetry(new RetryStrategyOptions<HttpResponseMessage>
            {
                MaxRetryAttempts = 2,
                BackoffType = DelayBackoffType.Exponential
            })
            .AddTimeout(new TimeoutStrategyOptions());
    });

    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Blazor host application unexpected shut down.");
    throw;
}
