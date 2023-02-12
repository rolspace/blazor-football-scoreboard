using System.Threading.Tasks;
using Football.Application.Common.Models;
using Microsoft.AspNetCore.SignalR;

namespace Football.Api.Hubs
{
    public class PlayHub : Hub
    {
        public async Task SendPlay(PlayDto play)
        {
            await Clients.All.SendAsync("ReceivePlay", play);
        }
    }
}
