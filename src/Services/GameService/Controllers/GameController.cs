using Football.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Football.Services.GameService.Controllers
{
    [Route("api/football/games")]
    public class GameController : Controller
    {
        private readonly IRepository _repository;

        public GameController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("week/{weekId}")]
        public async Task<ActionResult> GetGamesByWeek(int weekId)
        {
            ReadOnlyCollection<IGame> games = await _repository.GetGamesByWeek(weekId);

            return Ok(games);
        }
    }
}
