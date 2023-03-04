namespace Football.Worker.Providers;

public interface IHubProvider
{
    Task StartHubAsync();

    Task StopHubAsync();

    Task DisposeHubAsync();
}
