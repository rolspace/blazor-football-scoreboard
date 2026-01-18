namespace Football.Application.Interfaces;

public interface IHubFactory<out T>
{
    T CreateHub();
}
