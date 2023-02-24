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
    public async Task GetGamesById_ValidGameId_ReturnsHttpOk()
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
}
