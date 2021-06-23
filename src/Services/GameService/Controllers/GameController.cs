using Football.Core.Models;
using Football.Core.Persistence.Interfaces.DataProviders;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Football.Services.GameService.Controllers
{
    [Route("api/football/games")]
    public class GameController : Controller
    {
        private readonly IFootballDataProvider _dataProvider;

        public GameController(IFootballDataProvider repository)
        {
            _dataProvider = repository;
        }

        [EnableCors("CorsPolicy")]
        [HttpGet("week/{weekId}")]
        public async Task<ActionResult> GetGamesByWeek(int weekId)
        {
            ReadOnlyCollection<Game> games = await _dataProvider.GetGamesByWeek(weekId);

            return Ok(games);
        }
    }
}
