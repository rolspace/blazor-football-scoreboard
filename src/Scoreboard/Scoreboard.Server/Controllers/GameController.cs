using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Scoreboard.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        IAsyncRepository<Game> _gameRepository;

        public GameController(IAsyncRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet("week/{weekId}")]
        public async Task<IEnumerable<Game>> GetGamesByWeek([FromRoute] int weekId)
        {
            var games = await _gameRepository.ListAsync(game => game.Week == weekId);
            return games;
        }
    }
}
