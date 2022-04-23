using Football.Core.Models;
using Football.Core.Persistence.Interfaces.DataProviders;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
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

        [EnableCors("CorsPolicy")]
        [HttpGet("{gameId}")]
        public async Task<ActionResult> GetGameStats(int gameId)
        {
            ReadOnlyCollection<Stat> stats = await _dataProvider.GetGameStats(gameId);

            return Ok(stats);
        }
    }
}
