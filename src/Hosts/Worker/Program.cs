using Football.Worker;

var builder = WebApplication.CreateBuilder(args);
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
