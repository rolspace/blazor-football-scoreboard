using Football.Infrastructure.Extensions;
using Football.Worker;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHostedService<PlayLogBackgroundService>();

var app = builder.Build();

app.MapGet("/", () => new Response
{
    Message = "Web application for the PlayLogHostedService"
});

app.Run();

public class Response
{
    public string Message { get; set; } = string.Empty;
}
