namespace Football.Application.Interfaces;

public interface IHubManager
{
    Task StartAsync();

    Task StopAsync();

    Task DisposeAsync();
}
