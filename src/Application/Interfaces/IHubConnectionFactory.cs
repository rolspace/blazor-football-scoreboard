namespace Football.Application.Interfaces;

public interface IHubConnectionFactory<out T>
{
    T CreateHub();
}
