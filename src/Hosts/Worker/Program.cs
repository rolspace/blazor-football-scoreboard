using System.Reflection;
using Football.Infrastructure.Extensions;
using Football.Worker;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Worker host application start.");

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    if (builder.Environment.IsLocalhost())
    {
        builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
    }

    builder.Host.UseDefaultServiceProvider(options =>
    {
        bool validate = builder.Environment.IsDevelopment() || builder.Environment.IsLocalhost();
        options.ValidateScopes = validate;
        options.ValidateOnBuild = validate;
    });

    builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostBuilderContext.Configuration)
        .Enrich.FromLogContext()
    );

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);

    builder.Services.AddHostedService<PlayLogBackgroundService>();

    WebApplication app = builder.Build();

    app.MapGet("/", () => new WorkerResponse
    {
        Message = "Web application for the PlayLogHostedService."
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Worker host application could not be started.");
    throw;
}
finally
{
    Log.Information("Worker host application shut down.");
    Log.CloseAndFlush();
}
