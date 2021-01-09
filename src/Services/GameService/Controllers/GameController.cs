using Football.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Football.Services.GameService.Controllers
{
    [Route("api/games")]
    public class GameController : Controller
    {
        private readonly IRepository _repository;

        public GameController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{gameId}")]
        public IEnumerable<string> GetGameById(int gameId)
        {

            return new string[] { "value1", "value2" };
        }

        [HttpGet("week/{weekId}")]
        public async Task<ActionResult> GetGamesByWeek(int weekId)
        {
            ReadOnlyCollection<IGame> games = await _repository.GetGamesByWeek(weekId);

            return Ok(games);
        }
    }
}
