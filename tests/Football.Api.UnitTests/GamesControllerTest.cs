using FluentValidation;
using FluentValidation.Results;
using Football.Api.Controllers.V1;
using Football.Application.Features.Games;
using Football.Application.Features.Games.Models;
using Football.Application.Features.Stats;
using Football.Application.Features.Stats.Models;
using Football.Infrastructure.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Football.Api.UnitTests;

public class GamesControllerTest
{
    private Mock<ISender> _mockSender;

    private readonly Mock<IOptions<ScoreboardOptions>> _mockScoreboardOptions;

    private readonly Mock<ILogger<GamesController>> _mockLogger;

    private readonly Mock<IValidator<GetGameQuery>> _mockGetGameQueryValidator;

    private readonly Mock<IValidator<GetGamesQuery>> _mockGetGamesQueryValidator;

    private readonly Mock<IValidator<GetGameStatsQuery>> _mockGetGameStatsQueryValidator;

    public GamesControllerTest()
    {
        _mockSender = new Mock<ISender>();

        _mockScoreboardOptions = new Mock<IOptions<ScoreboardOptions>>();
        _mockScoreboardOptions.Setup(options => options.Value)
            .Returns(new ScoreboardOptions { Week = 1 });

        _mockLogger = new Mock<ILogger<GamesController>>();
        _mockGetGameQueryValidator = new Mock<IValidator<GetGameQuery>>();
        _mockGetGamesQueryValidator = new Mock<IValidator<GetGamesQuery>>();
        _mockGetGameStatsQueryValidator = new Mock<IValidator<GetGameStatsQuery>>();
    }

    [Fact]
    public async Task GetGameById_GameIdFound_ReturnsHttpOkAndGameDto()
    {
        var getGameQuery = new GetGameQuery { Id = 1 };
        var gameDto = new GameDto
        {
            Id = 1,
            Week = 1,
            HomeTeam = "HTM",
            AwayTeam = "ATM"
        };

        _mockSender.Setup(sender => sender.Send(getGameQuery, new CancellationToken()))
            .ReturnsAsync(gameDto);

        _mockGetGameQueryValidator.Setup(validator => validator.ValidateAsync(getGameQuery, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var gamesController = new GamesController(_mockSender.Object, _mockScoreboardOptions.Object, _mockLogger.Object,
            _mockGetGameQueryValidator.Object, _mockGetGamesQueryValidator.Object, _mockGetGameStatsQueryValidator.Object);

        ActionResult<GameDto> actionResult = await gamesController.GetGameById(getGameQuery);

        OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(gameDto, okResult.Value);
    }

    [Fact]
    public async Task GetGameById_GameIdNotFound_ReturnsNotFound()
    {
        var getGameQuery = new GetGameQuery { Id = 1 };

        _mockSender.Setup(sender => sender.Send<GameDto?>(getGameQuery, new CancellationToken()))
            .ReturnsAsync((GameDto?)null);

        _mockGetGameQueryValidator.Setup(validator => validator.ValidateAsync(getGameQuery, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var gamesController = new GamesController(_mockSender.Object, _mockScoreboardOptions.Object, _mockLogger.Object,
            _mockGetGameQueryValidator.Object, _mockGetGamesQueryValidator.Object, _mockGetGameStatsQueryValidator.Object);

        ActionResult<GameDto> actionResult = await gamesController.GetGameById(getGameQuery);

        NotFoundResult notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetGamesById_InvalidQuery_ReturnsBadRequest()
    {
        GetGameQuery getGameQuery = null!;

        _mockSender.Setup(sender => sender.Send(getGameQuery, new CancellationToken()));

        ValidationResult validationResult = new();
        validationResult.Errors.Add(new ValidationFailure("TestProperty", "Test Error Message"));

        _mockGetGameQueryValidator.Setup(validator => validator.ValidateAsync(getGameQuery, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        var gamesController = new GamesController(_mockSender.Object, _mockScoreboardOptions.Object, _mockLogger.Object,
            _mockGetGameQueryValidator.Object, _mockGetGamesQueryValidator.Object, _mockGetGameStatsQueryValidator.Object);

        ActionResult<GameDto> actionResult = await gamesController.GetGameById(getGameQuery);

        BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetGamesByWeek_WeekFound_ReturnsHttpOkAndGameDtoList()
    {
        var getGamesQuery = new GetGamesQuery { Week = 1 };
        var gameDtos = new List<GameDto> {
            new() {
                Id = 1,
                Week = 1,
                HomeTeam = "HT1",
                AwayTeam = "AT1"
            },
            new() {
                Id = 2,
                Week = 1,
                HomeTeam = "HT2",
                AwayTeam = "AT2"
            }
        };

        _mockSender.Setup(sender => sender.Send(getGamesQuery, new CancellationToken()))
            .ReturnsAsync(gameDtos);

        _mockGetGamesQueryValidator.Setup(validator => validator.ValidateAsync(getGamesQuery, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var gamesController = new GamesController(_mockSender.Object, _mockScoreboardOptions.Object, _mockLogger.Object,
            _mockGetGameQueryValidator.Object, _mockGetGamesQueryValidator.Object, _mockGetGameStatsQueryValidator.Object);

        ActionResult<IEnumerable<GameDto>> actionResult = await gamesController.GetGamesByWeek(getGamesQuery);

        OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(gameDtos, okResult.Value);
    }

    [Fact]
    public async Task GetGamesByWeek_WeekNotFound_ReturnsEmptyCollection()
    {
        var getGamesQuery = new GetGamesQuery { Week = 0 };

        _mockSender.Setup(sender => sender.Send(getGamesQuery, new CancellationToken()))
            .ReturnsAsync(new List<GameDto>());

        _mockGetGamesQueryValidator.Setup(validator => validator.ValidateAsync(getGamesQuery, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var gamesController = new GamesController(_mockSender.Object, _mockScoreboardOptions.Object, _mockLogger.Object,
            _mockGetGameQueryValidator.Object, _mockGetGamesQueryValidator.Object, _mockGetGameStatsQueryValidator.Object);

        ActionResult<IEnumerable<GameDto>> actionResult = await gamesController.GetGamesByWeek(getGamesQuery);

        OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(new List<GameDto>(), okResult.Value);
    }

    [Fact]
    public async Task GetGamesByWeek_InvalidQuery_ReturnsBadRequest()
    {
        GetGamesQuery getGamesQuery = null!;

        _mockSender.Setup(sender => sender.Send(getGamesQuery, new CancellationToken()));

        ValidationResult validationResult = new();
        validationResult.Errors.Add(new ValidationFailure("TestProperty", "Test Error Message"));

        _mockGetGamesQueryValidator.Setup(validator => validator.ValidateAsync(getGamesQuery, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        var gamesController = new GamesController(_mockSender.Object, _mockScoreboardOptions.Object, _mockLogger.Object,
            _mockGetGameQueryValidator.Object, _mockGetGamesQueryValidator.Object, _mockGetGameStatsQueryValidator.Object);

        ActionResult<IEnumerable<GameDto>> actionResult = await gamesController.GetGamesByWeek(getGamesQuery);

        BadRequestObjectResult BadRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetStatsById_GameIdFound_ReturnsGameStatsDtoModel()
    {
        var getGameStatsQuery = new GetGameStatsQuery() { Id = 1 };
        var gameStatDto = new GameStatDto
        {
            GameId = 1,
            Stats = new List<StatDto> {
                new() {
                    Team = "HTM",
                    Home = true,
                    Score = 7,
                    PassingYards = 100,
                    Sacks = 3,
                    Punts = 2,
                    ReturnYards = 20
                },
                new() {
                    Team = "ATM",
                    Home = false,
                    Score = 0,
                    PassingYards = 50,
                    Sacks = 1,
                    Punts = 4,
                    ReturnYards = 10
                }
            }
        };

        _mockSender.Setup(sender => sender.Send(getGameStatsQuery, new CancellationToken()))
            .ReturnsAsync(gameStatDto);

        _mockGetGameStatsQueryValidator.Setup(validator => validator.ValidateAsync(getGameStatsQuery, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var gamesController = new GamesController(_mockSender.Object, _mockScoreboardOptions.Object, _mockLogger.Object,
            _mockGetGameQueryValidator.Object, _mockGetGamesQueryValidator.Object, _mockGetGameStatsQueryValidator.Object);

        ActionResult<GameStatDto> actionResult = await gamesController.GetStatsById(getGameStatsQuery);

        OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(gameStatDto, okResult.Value);
    }
}
