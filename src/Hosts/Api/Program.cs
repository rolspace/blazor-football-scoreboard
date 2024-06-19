using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Football.Api.Hubs;
using Football.Api.Options;
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

    builder.Services
        .AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1.0);
        })
        .AddMvc()
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

    builder.Services.AddSwaggerGen(options =>
    {
        options.DescribeAllParametersInCamelCase();
    });

    CorsOptions? corsOptions = builder.Configuration.GetSection(CorsOptions.Key).Get<CorsOptions>();
    if (corsOptions is not null)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: corsOptions.PolicyName, poliyBuilder =>
            {
                poliyBuilder
                    .WithOrigins(corsOptions.AllowedOrigins)
                    .WithMethods(corsOptions.AllowedMethods)
                    .AllowAnyHeader();
            });
        });
    }

    WebApplication app = builder.Build();

    if (app.Environment.IsDevelopment() || app.Environment.IsLocalhost())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (string groupName in app.DescribeApiVersions().Select(a => a.GroupName))
            {
                string url = $"/swagger/{groupName}/swagger.json";
                string name = groupName.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
            }
        });
    }

    app.UseSerilogRequestLogging();
    app.UseRouting();

    if (corsOptions is not null)
    {
        app.UseCors(corsOptions.PolicyName);
    }

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHealthChecks("/health");
        endpoints.MapHub<PlayHub>("/hub/plays");
        endpoints.MapControllers();
    });

    await app.RunAsync();

    Log.Information("API host application shut down.");
}
catch (Exception ex)
{
    Log.Fatal(ex, "API host application unexpected shut down.");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
