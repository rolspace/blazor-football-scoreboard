using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Football.Application.Interfaces;
using Football.Blazor;
using Football.Infrastructure.Factories;
using Football.Infrastructure.Options;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using Serilog;

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

    builder.Services.Configure<HubOptions>(builder.Configuration.GetSection(HubOptions.Key));

    builder.Services.AddSingleton<IHubConnectionFactory<HubConnection>, HubConnectionFactory>();

    builder.Services.AddHttpClient(Constants.DefaultHttpClient, (serviceProvider, client) =>
    {
        string? baseAddress = builder.Configuration.GetValue<string>("HttpClient:BaseAddress");

        if (string.IsNullOrWhiteSpace(baseAddress))
        {
             throw new InvalidOperationException("The HttpClient BaseAddress has not been configured.");
        }

        client.BaseAddress = new Uri(baseAddress);
    }).AddResilienceHandler(Constants.DefaultPipeline, handlerBuilder =>
    {
        RetryStrategyOptions<HttpResponseMessage>? retryOptions = builder.Configuration.GetSection("HttpClient:RetryStrategy")
            .Get<RetryStrategyOptions<HttpResponseMessage>>();

        handlerBuilder
            .AddRetry(retryOptions ?? new RetryStrategyOptions<HttpResponseMessage>())
            .AddTimeout(new TimeoutStrategyOptions());
    });

    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Blazor host application unexpected shut down.");
    throw;
}
