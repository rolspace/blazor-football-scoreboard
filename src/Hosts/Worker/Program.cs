using System.Reflection;
using Football.Worker;
using Football.Worker.Extensions;
using Football.Worker.Providers;
using Football.Worker.Settings;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment() || builder.Environment.IsLocalhost())
{
    builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
}

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddTransient<IHubProvider>((serviceProvider) =>
{
    var hubSettings = builder.Configuration.GetSection(HubSettings.Hub).Get<HubSettings>();
    var hubUri = new Uri(hubSettings.Endpoint);

    return new HubProvider(hubUri);
});
builder.Services.AddHostedService<PlayLogHostedService>();

var app = builder.Build();

app.MapGet("/", () => new Response
{
    Message = "StreamService is running"
});

app.Run();

public class Response
{
    public string Message { get; set; } = string.Empty;
}
