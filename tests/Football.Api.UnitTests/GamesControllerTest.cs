using Football.Api.Controllers.V1;
using Football.Application.Common.Models;
using Football.Application.Queries.Games;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Football.Api.UnitTests;

public class UnitTest1
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
    public async Task GetGamesById_NonExistingGameId_ReturnsBadRequest()
    {
        GetGameQuery? getGameQuery = null!;

        var mockSender = new Mock<ISender>();
        mockSender.Setup<Task<GameDto?>>(sender => sender.Send<GameDto?>(getGameQuery, new CancellationToken()));

        var gamesController = new GamesController(mockSender.Object);
        var actionResult = await gamesController.GetGameById(getGameQuery);

        var notFoundResult = Assert.IsType<BadRequestResult>(actionResult.Result);
    }
}
