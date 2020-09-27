using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Scoreboard.Server.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendPlay(Play play)
        {
            await Clients.All.SendAsync("ReceivePlay", play);
        }
    }
}
