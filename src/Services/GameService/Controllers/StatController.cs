using Football.Core.Models;
using Football.Core.Persistence.Interfaces.DataProviders;
using Microsoft.AspNetCore.Mvc;

namespace Football.Services.GameService.Controllers
{
    [Route("api/football/stats")]
    public class StatController : Controller
    {
        private readonly IFootballDataProvider _dataProvider;

        public StatController(IFootballDataProvider repository)
        {
            _dataProvider = repository;
        }

        [HttpPatch]
        public ActionResult Patch([FromBody] Play play)
        {
            // await _dataProvider.SaveStat(currentStat);

            return Ok();
        }
    }
}
