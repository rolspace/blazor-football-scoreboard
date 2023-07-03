using Football.Api.Controllers.V1;
using Football.Application.Features.Games;
using Football.Application.Features.Games.Models;
using Football.Application.Features.Stats;
using Football.Application.Features.Stats.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Football.Api.UnitTests;

public class GamesControllerTest
{
    [Fact]
    public async Task GetGamesById_ValidGameId_ReturnsHttpOkAndGameDto()
    {
        var getGameQuery = new GetGameQuery { Id = 1 };
        var gameDto = new GameDto
        {
            Id = 1,
            Week = 1,
            HomeTeam = "HTM",
            AwayTeam = "ATM"
        };

        var mockSender = new Mock<ISender>();
        mockSender.Setup(sender => sender.Send(getGameQuery, new CancellationToken()))
            .ReturnsAsync(gameDto);

        var gamesController = new GamesController(mockSender.Object);
        var actionResult = await gamesController.GetGameById(getGameQuery);

        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(gameDto, okResult.Value);
    }

    [Fact]
    public async Task GetGamesById_NonExistingGameId_ReturnsNotFound()
    {
        var getGameQuery = new GetGameQuery { Id = 0 };

        var mockSender = new Mock<ISender>();
        mockSender.Setup<Task<GameDto?>>(sender => sender.Send<GameDto?>(getGameQuery, new CancellationToken()))
            .ReturnsAsync((GameDto?)null);

        var gamesController = new GamesController(mockSender.Object);
        var actionResult = await gamesController.GetGameById(getGameQuery);

        var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetGamesById_InvalidQuery_ReturnsBadRequest()
    {
        GetGameQuery getGameQuery = null!;

        var mockSender = new Mock<ISender>();
        mockSender.Setup(sender => sender.Send(getGameQuery, new CancellationToken()));

        var gamesController = new GamesController(mockSender.Object);
        var actionResult = await gamesController.GetGameById(getGameQuery);

        var notFoundResult = Assert.IsType<BadRequestResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetGamesByWeek_WeekExists_ReturnsHttpOkAndGameDtoList()
    {
        var getGamesQuery = new GetGamesQuery { Week = 1 };
        var gameDtos = new List<GameDto> {
            new GameDto
            {
                Id = 1,
                Week = 1,
                HomeTeam = "HT1",
                AwayTeam = "AT1"
            },
            new GameDto
            {
                Id = 2,
                Week = 1,
                HomeTeam = "HT2",
                AwayTeam = "AT2"
            }
        };

        var mockSender = new Mock<ISender>();
        mockSender.Setup(sender => sender.Send(getGamesQuery, new CancellationToken()))
            .ReturnsAsync(gameDtos);

        var gamesController = new GamesController(mockSender.Object);
        var actionResult = await gamesController.GetGamesByWeek(getGamesQuery);

        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(gameDtos, okResult.Value);
    }

    [Fact]
    public async Task GetGamesByWeek_NonExistingWeek_ReturnsNotFound()
    {
        var getGamesQuery = new GetGamesQuery { Week = 0 };

        var mockSender = new Mock<ISender>();
        mockSender.Setup(sender => sender.Send(getGamesQuery, new CancellationToken()))
            .ReturnsAsync(new List<GameDto>());

        var gamesController = new GamesController(mockSender.Object);
        var actionResult = await gamesController.GetGamesByWeek(getGamesQuery);

        var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetGamesByWeek_InvalidQuery_ReturnsBadRequest()
    {
        GetGamesQuery getGamesQuery = null!;

        var mockSender = new Mock<ISender>();
        mockSender.Setup(sender => sender.Send(getGamesQuery, new CancellationToken()));

        var gamesController = new GamesController(mockSender.Object);
        var actionResult = await gamesController.GetGamesByWeek(getGamesQuery);

        var notFoundResult = Assert.IsType<BadRequestResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetStatsById_ValidGameId_ReturnsGameStatsDtoModel()
    {
        var getGameStatsQuery = new GetGameStatsQuery() { Id = 1 };
        var gameStatDto = new GameStatDto
        {
            GameId = 1,
            Stats = new List<StatDto> {
                new StatDto
                {
                    Team = "HTM",
                    Home = true,
                    Score = 7,
                    PassingYards = 100,
                    Sacks = 3,
                    ReturnYards = 20,
                    Punts = 2
                },
                new StatDto
                {
                    Team = "ATM",
                    Home = false,
                    Score = 0,
                    PassingYards = 50,
                    Sacks = 1,
                    ReturnYards = 10,
                    Punts = 4
                }
            }
        };

        var mockSender = new Mock<ISender>();
        mockSender.Setup(sender => sender.Send(getGameStatsQuery, new CancellationToken()))
            .ReturnsAsync(gameStatDto);

        var gamesController = new GamesController(mockSender.Object);
        var actionResult = await gamesController.GetStatsById(getGameStatsQuery);

        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(gameStatDto, okResult.Value);
    }
}
