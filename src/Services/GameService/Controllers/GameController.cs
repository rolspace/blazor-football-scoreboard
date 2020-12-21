using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GameService.Controllers
{
    [Route("api/games")]
    public class GameController : Controller
    {
        private readonly IRepository<Game> _gameRepository;

        public GameController(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet("{gameId}")]
        public IEnumerable<string> GetGameById(int gameId)
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("week/{weekId}")]
        public IEnumerable<string> GetGamesByWeek(int weekId)
        {
            return new string[] { "value1", "value2" };
        }
    }
}
