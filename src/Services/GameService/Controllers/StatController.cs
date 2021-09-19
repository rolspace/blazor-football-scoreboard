using Football.Core.Models;
using Football.Core.Persistence.Interfaces.DataProviders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Football.Services.GameService.Controllers
{
    [Route("api/football/stat")]
    public class StatController : Controller
    {
        private readonly ILogger<StatController> _logger;
        private readonly IFootballDataProvider _dataProvider;

        public StatController(IFootballDataProvider dataProvider, ILogger<StatController> logger)
        {
            _dataProvider = dataProvider;
            _logger = logger;
        }

        [HttpPut("{gameId}/{team}")]
        public async Task<ActionResult> PutStat(int gameId, string team, [FromBody] PlayLog playLog)
        {
            var stat = new Stat
            {
                GameId = gameId,
                Team = team,
                Score = playLog.Score
            };

            if (playLog.OffensePlayLog != null)
            {
                stat.AirYards = playLog.OffensePlayLog.AirYards;
            }

            if (playLog.DefensePlayLog != null)
            {
                stat.Sacks = playLog.DefensePlayLog.Sacks;
            }

            if (playLog.SpecialPlayLog != null)
            {
                stat.Punts = playLog.SpecialPlayLog.Punts;
                stat.ReturnYards = playLog.SpecialPlayLog.ReturnYards;
            }

            await _dataProvider.SaveStat(stat);

            return Ok();
        }
    }
}
