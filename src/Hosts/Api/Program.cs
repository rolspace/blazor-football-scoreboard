using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Began Application startup");

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostBuilderContext.Configuration)
        .Enrich.FromLogContext()
    );

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();

    app.MapControllers();

    Log.Information("Completed application startup");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Failed application startup");
}
finally
{
    Log.Information("Completed application shut down");
    Log.CloseAndFlush();
}
