using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Scoreboard.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        IRepository<Game> _gameRepository;

        public GameController(IRepository<Game> gameRepository)
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
