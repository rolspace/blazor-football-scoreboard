using System.Threading.Tasks;
using Football.Application.Models;
using Microsoft.AspNetCore.SignalR;

namespace Football.Api.Hubs
{
    public class PlayHub : Hub
    {
        public async Task SendPlay(PlayDto playDto)
        {
            await Clients.All.SendAsync("ReceivePlay", playDto);
        }
    }
}
