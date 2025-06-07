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

    HttpClientOptions httpClientOptions = builder.Configuration.GetSection(HttpClientOptions.Key)
        .Get<HttpClientOptions>() ?? throw new InvalidOperationException("The HttpClientOptions have not been configured.");

    builder.Services.AddHttpClient(Constants.DefaultHttpClient, (serviceProvider, client) =>
    {
        string? baseAddress = httpClientOptions.BaseAddress;

        if (string.IsNullOrWhiteSpace(baseAddress))
        {
             throw new InvalidOperationException("The HttpClient BaseAddress has not been configured.");
        }

        client.BaseAddress = new Uri(baseAddress);
    }).AddResilienceHandler("DefaultPipeline", handlerBuilder =>
    {
        RetryStrategyOptions<HttpResponseMessage>? retryOptions = new()
        {
            MaxRetryAttempts = httpClientOptions.RetryOptions.MaxRetryAttempts,
            BackoffType = httpClientOptions.RetryOptions.BackoffType
        };

        handlerBuilder
            .AddRetry(retryOptions)
            .AddTimeout(new TimeoutStrategyOptions());
    });

    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Blazor host application unexpected shut down.");
    throw;
}
