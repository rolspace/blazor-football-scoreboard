using System.Reflection;
using Football.Api.Hubs;
using Football.Api.Settings;
using Football.Infrastructure.Extensions;
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
    Log.Information("API host application start.");

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

    builder.Services.AddSignalR();
    builder.Services.AddControllers();
    builder.Services.AddHealthChecks();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    CorsSettings? corsSettings = builder.Configuration.GetSection(CorsSettings.Key).Get<CorsSettings>();
    if (corsSettings is not null)
    {
        builder.Services.AddCors(corsOptions =>
        {
            corsOptions.AddPolicy(name: corsSettings.PolicyName, poliyBuilder =>
            {
                poliyBuilder
                    .WithOrigins(corsSettings.AllowedOrigins)
                    .WithMethods(corsSettings.AllowedMethods)
                    .AllowAnyHeader();
            });
        });
    }

    WebApplication app = builder.Build();

    if (app.Environment.IsDevelopment() || app.Environment.IsLocalhost())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseRouting();

    if (corsSettings is not null)
    {
        app.UseCors(corsSettings.PolicyName);
    }

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHealthChecks("/health");
        endpoints.MapHub<PlayHub>("/hub/plays");
        endpoints.MapControllers();
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "API host application could not be started.");
    throw;
}
finally
{
    Log.Information("API host application shut down.");
    Log.CloseAndFlush();
}
