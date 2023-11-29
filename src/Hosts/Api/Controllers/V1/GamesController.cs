using Football.Application.Features.Games;
using Football.Application.Features.Games.Models;
using Football.Application.Features.Stats;
using Football.Application.Features.Stats.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Football.Api.Controllers.V1;

[ApiController]
[Route("api/v1/games")]
public class GamesController : ControllerBase
{
    private const int DEFAULT_WEEK = 1;

    private const string WeekKey = "Week";

    private readonly int _week;

    private readonly ISender _mediator;

    public GamesController(IConfiguration configuration, ISender mediator)
    {
        if (configuration is not null)
        {
            _week = configuration.GetValue(WeekKey, DEFAULT_WEEK);
        }

        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GameDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesByWeek([FromQuery] GetGamesQuery query)
    {
        if (query == null) return BadRequest();

        IEnumerable<GameDto> games = await _mediator.Send(query);

        if (!games.Any()) return NotFound();

        return Ok(games);
    }

    [HttpGet("today")]
    [ProducesResponseType(typeof(IEnumerable<GameDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetCurrentGames()
    {
        GetGamesQuery query = new()
        {
            Week = _week
        };

        IEnumerable<GameDto> games = await _mediator.Send(query);

        if (!games.Any()) return NotFound();

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
