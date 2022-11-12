using System;
using Football.Core.Persistence.Contexts;
using Football.Core.Persistence.Interfaces.DataProviders;
using Football.Core.Persistence.MySql;
using Football.Workers.GameWorker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Application startup starting");

    var builder = Host.CreateDefaultBuilder(args);

    builder.ConfigureServices((hostBuildContext, services) =>
    {
        services.AddHostedService<Worker>();

        var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 28));
        services.AddDbContext<FootballDbContext>(options =>
            options.UseMySql(hostBuildContext.Configuration.GetConnectionString("FootballDbContext"), mySqlServerVersion));

        services.AddScoped<IFootballDataProvider, MySqlFootballDataProvider>();
    })
    .UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostBuilderContext.Configuration)
        .Enrich.FromLogContext()
    );

    builder.Build().Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.Information("Application shut down complete");
    Log.CloseAndFlush();
}
