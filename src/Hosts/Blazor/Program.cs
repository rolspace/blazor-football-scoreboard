using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Football.Application.Interfaces;
using Football.Blazor;
using Football.Blazor.Options;
using Football.Infrastructure.Factories;
using Football.Infrastructure.Options;
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

    ApiOptions? apiOptions = builder.Configuration
        .GetSection(ApiOptions.Key)
        .Get<ApiOptions>();

    builder.Services.Configure<HubOptions>(builder.Configuration.GetSection(HubOptions.Key));

    builder.Services.AddSingleton(apiOptions ?? new ApiOptions());
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    builder.Services.AddSingleton<IHubConnectionFactory<HubConnection>, HubConnectionFactory>();

    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Blazor host application unexpected shut down.");
    throw;
}
