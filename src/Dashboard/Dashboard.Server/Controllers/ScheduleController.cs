using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Games.Entities;
using Games.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dashboard.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        IAsyncRepository<Game> _gameRepository;

        public ScheduleController(IAsyncRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet("week/{weekId}")]
        public async Task<IEnumerable<Game>> GetGamesByWeek([FromRoute] int weekId)
        {
            var games = await _gameRepository.ListAsync(game => game.Week == weekId);
            return games;
        }
    }
}
