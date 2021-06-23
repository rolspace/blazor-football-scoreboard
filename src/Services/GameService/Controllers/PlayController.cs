using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Football.Core.Models;
using Football.Core.Persistence.Interfaces.DataProviders;
using Microsoft.AspNetCore.Mvc;

namespace Football.Services.GameService.Controllers
{
    [Route("api/football/plays")]
    public class PlayController : Controller
    {
        private readonly IFootballDataProvider _dataProvider;

        public PlayController(IFootballDataProvider repository)
        {
            _dataProvider = repository;
        }

        [HttpGet("{weekId}/{start}/{end}")]
        public async Task<ActionResult> GetPlaysByWeekAndGameTime(int weekId, int start, int end)
        {
            ReadOnlyCollection<Play> plays = await _dataProvider.GetPlaysByWeekAndGameTime(weekId, start, end);

            return Ok(plays);
        }
    }
}
