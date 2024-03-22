using Asp.Versioning;
using Football.Application.Features.Games;
using Football.Application.Features.Games.Models;
using Football.Application.Features.Stats;
using Football.Application.Features.Stats.Models;
using Football.Infrastructure.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Football.Api.Controllers.V1;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/games")]
public class GamesController : ControllerBase
{
    private readonly ScoreboardOptions _scoreboardOptions;

    private readonly ISender _mediator;

    public GamesController(ISender mediator, IOptions<ScoreboardOptions> scoreboardOptions)
    {
        _mediator = mediator;

        _scoreboardOptions = scoreboardOptions.Value;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GameDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesByWeek([FromQuery] GetGamesQuery query)
    {
        if (query == null) return BadRequest();

        IEnumerable<GameDto> games = await _mediator.Send(query);

        return Ok(games);
    }

    [HttpGet("now")]
    [ProducesResponseType(typeof(IEnumerable<GameDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetCurrentGames()
    {
        GetGamesQuery query = new()
        {
            Week = _scoreboardOptions.Week
        };

        IEnumerable<GameDto> games = await _mediator.Send(query);

        return Ok(games);
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

    [HttpGet("{id}/stats")]
    [ProducesResponseType(typeof(GameStatDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GameStatDto>> GetStatsById([FromRoute] GetGameStatsQuery query)
    {
        if (query is null) return BadRequest();

        GameStatDto gameStats = await _mediator.Send(query);

        return Ok(gameStats);
    }
}
