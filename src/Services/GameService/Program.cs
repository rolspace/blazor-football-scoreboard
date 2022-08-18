using System;
using Football.Core.Persistence.Interfaces.DataProviders;
using Football.Core.Persistence.MySql;
using Football.Core.Persistence.MySql.Contexts;
using Football.Services.GameService.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(hostBuilderContext.Configuration));

    var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 28));

    builder.Services.AddDbContext<FootballDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("FootballDbContext"), mySqlServerVersion));

    builder.Services.AddScoped<IFootballDataProvider, MySqlFootballDataProvider>();
    builder.Services.AddSignalR();
    builder.Services.AddControllers();
    builder.Services.AddHealthChecks();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service.Game", Version = "v1" });
    });

    WebApplication app = builder.Build();

    if (builder.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service.Game v1"));
    }

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors(corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins(builder.Configuration["AllowedOrigin"])
            .AllowAnyHeader()
            .WithMethods("GET", "POST")
            .AllowCredentials();
    });

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHealthChecks("/health");
        endpoints.MapHub<GameHub>("/hub/football/game");
        endpoints.MapControllers();
    });

    app.Run();
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
