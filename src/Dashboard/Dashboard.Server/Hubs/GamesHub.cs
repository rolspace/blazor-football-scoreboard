using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Dashboard.Server.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendPlay(GameState gameState)
        {
            await Clients.All.SendAsync("ReceivePlay", gameState);
        }
    }
}
