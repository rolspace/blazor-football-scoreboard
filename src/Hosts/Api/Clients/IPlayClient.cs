using Football.Application.Features.Plays.Models;

namespace Football.Api.Clients;

public interface IPlayClient
{
    Task ReceivePlay(PlayDto playDto);
}
