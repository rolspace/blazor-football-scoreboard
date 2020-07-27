using System.Threading.Tasks;
using Core.Shared;
using Microsoft.AspNetCore.SignalR;

namespace Dashboard.Server.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendPlay(GamePlay gamePlay)
        {
            await Clients.All.SendAsync("ReceivePlay", gamePlay);
        }
    }
}
