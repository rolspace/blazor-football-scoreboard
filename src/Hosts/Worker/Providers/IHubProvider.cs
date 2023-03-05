namespace Football.Worker.Providers;

public interface IHubProvider
{
    Task StartAsync();

    Task StopAsync();

    Task DisposeAsync();
}
