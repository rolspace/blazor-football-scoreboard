using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Football.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Football.Services.GameService.Controllers
{
    [Route("api/football/plays")]
    public class PlayController : Controller
    {
        private readonly IRepository _repository;

        public PlayController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("week/{weekId}/{start}/{end}")]
        public async Task<ActionResult> GetGameById(int weekId, int start, int end)
        {
            ReadOnlyCollection<IPlay> plays = await _repository.GetPlaysByGameTime(weekId, start, end);

            return Ok(plays);
        }
    }
}
