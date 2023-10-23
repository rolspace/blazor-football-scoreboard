using Football.Application.Interfaces;
using Football.Blazor;
using Football.Blazor.Settings;
using Football.Infrastructure.Hub;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
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

    var scoreboardSettings = builder.Configuration
        .GetSection(ScoreboardSettings.Key)
        .Get<ScoreboardSettings>();

    builder.Services.Configure<HubSettings>(builder.Configuration.GetSection(HubSettings.Key));

    builder.Services.AddSingleton(scoreboardSettings ?? new ScoreboardSettings());
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    builder.Services.AddSingleton<IHubManager, HubManager>();

    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Blazor host application terminated unexpectedly.");
    throw;
}
