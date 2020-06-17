using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Dashboard.Server.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendPlay(string description)
        {
            await Clients.All.SendAsync("ReceivePlay", description);
        }
    }
}
