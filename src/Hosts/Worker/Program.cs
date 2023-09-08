using Football.Infrastructure.Extensions;
using Football.Worker;
using Serilog;
using Serilog.Events;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Application start.");

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

    var app = builder.Build();

    app.MapGet("/", () => new Response
    {
        Message = "Web application for the PlayLogHostedService"
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly.");
}
finally
{
    Log.Information("Application shut down.");
    Log.CloseAndFlush();
}

public class Response
{
    public string Message { get; set; } = string.Empty;
}
