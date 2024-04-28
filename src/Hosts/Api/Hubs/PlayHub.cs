using Football.Api.Clients;
using Football.Application.Features.Plays.Models;
using Microsoft.AspNetCore.SignalR;

namespace Football.Api.Hubs;

public class PlayHub : Hub<IPlayClient>
{
    public async Task SendPlay(PlayDto playDto)
    {
        await Clients.All.ReceivePlay(playDto);
    }
}
