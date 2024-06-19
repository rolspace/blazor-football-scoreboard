using Asp.Versioning;
using FluentValidation;
using FluentValidation.Results;
using Football.Api.Models.Errors;
using Football.Application.Features.Games;
using Football.Application.Features.Games.Models;
using Football.Application.Features.Stats;
using Football.Application.Features.Stats.Models;
using Football.Application.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Football.Api.Controllers.V1;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/games")]
public class GamesController : ControllerBase
{
    private readonly ISender _mediator;

    private readonly ScoreboardOptions _scoreboardOptions;

    private readonly IValidator<GetGameQuery> _getGameQueryValidator;

    private readonly IValidator<GetGamesQuery> _getGamesQueryValidator;

    private readonly IValidator<GetGameStatsQuery> _getGameStatsQueryValidator;

    public GamesController(ISender mediator,
        IOptions<ScoreboardOptions> scoreboardOptions,
        IValidator<GetGameQuery> getGameQueryValidator,
        IValidator<GetGamesQuery> getGamesQueryValidator,
        IValidator<GetGameStatsQuery> getGameStatsQueryValidator)
    {
        _mediator = mediator;
        _getGameQueryValidator = getGameQueryValidator;
        _getGamesQueryValidator = getGamesQueryValidator;
        _getGameStatsQueryValidator = getGameStatsQueryValidator;
        _scoreboardOptions = scoreboardOptions.Value;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GameDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesByWeek([FromQuery] GetGamesQuery query)
    {
        ValidationResult validationResult = await _getGamesQueryValidator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            BadRequestInvalidParamsDetails problemDetails = new(validationResult.Errors);

            return BadRequest(problemDetails);
        }

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
        ValidationResult validationResult = await _getGameQueryValidator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            BadRequestInvalidParamsDetails problemDetails = new(validationResult.Errors);

            return BadRequest(problemDetails);
        }

        GameDto game = await _mediator.Send(query);

        if (game is null) return NotFound();

        return Ok(game);
    }

    [HttpGet("{id}/stats")]
    [ProducesResponseType(typeof(GameStatDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GameStatDto>> GetStatsById([FromRoute] GetGameStatsQuery query)
    {
        ValidationResult validationResult = await _getGameStatsQueryValidator.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            BadRequestInvalidParamsDetails problemDetails = new(validationResult.Errors);

            return BadRequest(problemDetails);
        }

        GameStatDto gameStats = await _mediator.Send(query);

        if (gameStats is null) return NotFound();

        return Ok(gameStats);
    }
}
