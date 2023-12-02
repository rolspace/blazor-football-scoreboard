using Football.Application.Interfaces;
using Football.Blazor;
using Football.Blazor.Settings;
using Football.Infrastructure.Hub;
using Football.Infrastructure.Options;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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

    ScoreboardSettings? scoreboardSettings = builder.Configuration
        .GetSection(ScoreboardSettings.Key)
        .Get<ScoreboardSettings>();

    builder.Services.Configure<HubOptions>(builder.Configuration.GetSection(HubOptions.Key));

    builder.Services.AddSingleton(scoreboardSettings ?? new ScoreboardSettings());
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    builder.Services.AddSingleton<IHubProvider, HubProvider>();

    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Blazor host application terminated unexpectedly.");
    throw;
}
