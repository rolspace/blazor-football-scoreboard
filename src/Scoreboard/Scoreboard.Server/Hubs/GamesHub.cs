using System.Collections.Generic;
using System.Threading.Tasks;
using Football.Core.Models;
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
