using Football.Application.Common.Models;
using Football.Application.Games.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Football.Api.Controllers;

[ApiController]
[Route("api/games")]
public class GamesController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GameDto>> GetGameById([FromRoute] GetGameQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
