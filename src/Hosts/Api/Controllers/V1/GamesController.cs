using Football.Application.Games.Queries;
using Football.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Football.Api.Controllers.V1;

[ApiController]
[Route("api/v1/games")]
public class GamesController : ControllerBase
{
    private ISender _mediator;

    public GamesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GameDto>> GetGameById([FromRoute] GetGameQuery query)
    {
        if (query == null) return BadRequest();

        GameDto game = await _mediator.Send(query);

        if (game == null) return NotFound();

        return Ok(game);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GameDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesByWeek([FromQuery] GetGamesQuery query)
    {
        if (query == null) return BadRequest();

        IEnumerable<GameDto> games = await _mediator.Send(query);

        if (games.Count() == 0) return NotFound();

        return Ok(games);
    }
}
