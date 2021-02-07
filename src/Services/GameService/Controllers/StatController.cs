using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Football.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Football.Services.GameService.Controllers
{
    [Route("api/football/stats")]
    public class StatController : Controller
    {
        private readonly IRepository _repository;

        public StatController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{gameId}/{team}")]
        public async Task<ActionResult> GetStatsByGameAndTeam(int gameId, string team)
        {
            ReadOnlyCollection<IStat> stats = await _repository.GetStatsByGameAndTeam(gameId, team);

            return Ok(stats);
        }

        // WRITE PATCH METHOD
    }
}
