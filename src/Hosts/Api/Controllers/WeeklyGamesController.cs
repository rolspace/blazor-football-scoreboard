using Football.Application.Common.Models;
using Football.Application.Games.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Football.Api.Controllers;

[ApiController]
[Route("api/weekly-games")]
public class WeeklyScheduleController : ApiControllerBase
{
    [HttpGet("{week}")]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesByWeek([FromRoute] GetGamesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
