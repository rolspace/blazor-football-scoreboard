using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Football.Core.Models;
using Football.Core.Persistence.Interfaces.DataProviders;
using Microsoft.AspNetCore.Mvc;

namespace Football.Services.GameService.Controllers
{
    [Route("api/football/games")]
    public class GameController : Controller
    {
        private readonly IFootballDataProvider _dataProvider;

        public GameController(IFootballDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        [HttpGet("week/{weekId}")]
        public async Task<ActionResult> GetGamesByWeek(int weekId)
        {
            ReadOnlyCollection<Game> games = await _dataProvider.GetGamesByWeek(weekId);

            return Ok(games);
        }
    }
}
