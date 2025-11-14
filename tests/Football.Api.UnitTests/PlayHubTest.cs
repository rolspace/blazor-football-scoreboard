using Football.Api.Clients;
using Football.Api.Hubs;
using Football.Application.Features.Plays.Models;
using Microsoft.AspNetCore.SignalR;
using Moq;

namespace Football.Api.UnitTests;

public class PlayHubTest
{
    private readonly Mock<IHubCallerClients<IPlayClient>> _mockClients;
    private readonly Mock<IPlayClient> _mockPlayClient;
    private readonly PlayHub _playHub;

    public PlayHubTest()
    {
        _mockClients = new Mock<IHubCallerClients<IPlayClient>>();
        _mockPlayClient = new Mock<IPlayClient>();

        _mockClients.Setup(clients => clients.All)
            .Returns(_mockPlayClient.Object);

        _playHub = new PlayHub
        {
            Clients = _mockClients.Object
        };
    }

    [Fact]
    public async Task SendPlay_ValidPlayDto_BroadcastsToAllClients()
    {
        // Arrange
        var playDto = new PlayDto
        {
            Id = 1,
            GameId = 100,
            HomeTeam = "KC",
            HomeScore = 14,
            AwayTeam = "SF",
            AwayScore = 10,
            Quarter = 2,
            QuarterSecondsRemaining = 300,
            GameOver = false,
            Description = "Patrick Mahomes pass complete to Travis Kelce for 15 yards",
            PlayType = "Pass",
            YardsGained = 15,
            Sack = false,
            Kickoff = false,
            PuntAttempt = false,
            Punt = false,
            ReturnYards = null,
            HomeTeamPossession = true,
            HomeTeamOnOffense = true,
            AwayTeamOnOffense = false
        };

        // Act
        await _playHub.SendPlay(playDto);

        // Assert
        _mockPlayClient.Verify(
            client => client.ReceivePlay(playDto),
            Times.Once,
            "ReceivePlay should be called exactly once with the provided PlayDto"
        );
    }

    [Fact]
    public async Task SendPlay_MultiplePlayDtos_BroadcastsEachToAllClients()
    {
        // Arrange
        var playDto1 = new PlayDto
        {
            Id = 1,
            GameId = 100,
            HomeTeam = "KC",
            HomeScore = 0,
            AwayTeam = "SF",
            AwayScore = 0,
            Quarter = 1,
            QuarterSecondsRemaining = 900,
            GameOver = false,
            Description = "Kickoff",
            Kickoff = true
        };

        var playDto2 = new PlayDto
        {
            Id = 2,
            GameId = 100,
            HomeTeam = "KC",
            HomeScore = 7,
            AwayTeam = "SF",
            AwayScore = 0,
            Quarter = 1,
            QuarterSecondsRemaining = 850,
            GameOver = false,
            Description = "Touchdown",
            HomeTeamOnOffense = true
        };

        // Act
        await _playHub.SendPlay(playDto1);
        await _playHub.SendPlay(playDto2);

        // Assert
        _mockPlayClient.Verify(
            client => client.ReceivePlay(playDto1),
            Times.Once,
            "ReceivePlay should be called with first PlayDto"
        );

        _mockPlayClient.Verify(
            client => client.ReceivePlay(playDto2),
            Times.Once,
            "ReceivePlay should be called with second PlayDto"
        );
    }

    [Fact]
    public async Task SendPlay_GameOverPlayDto_BroadcastsToAllClients()
    {
        // Arrange
        var playDto = new PlayDto
        {
            Id = 999,
            GameId = 100,
            HomeTeam = "KC",
            HomeScore = 31,
            AwayTeam = "SF",
            AwayScore = 20,
            Quarter = 4,
            QuarterSecondsRemaining = 0,
            GameOver = true,
            Description = "End of Game",
            PlayType = null,
            YardsGained = 0
        };

        // Act
        await _playHub.SendPlay(playDto);

        // Assert
        _mockPlayClient.Verify(
            client => client.ReceivePlay(It.Is<PlayDto>(p => p.GameOver == true)),
            Times.Once,
            "ReceivePlay should be called with GameOver set to true"
        );
    }

    [Fact]
    public async Task SendPlay_PlayDtoWithNullableProperties_BroadcastsToAllClients()
    {
        // Arrange
        var playDto = new PlayDto
        {
            Id = 5,
            GameId = 200,
            HomeTeam = "NE",
            HomeScore = 3,
            AwayTeam = "BUF",
            AwayScore = 7,
            Quarter = 3,
            QuarterSecondsRemaining = 450,
            GameOver = false,
            Description = "Incomplete pass",
            PlayType = "Pass",
            YardsGained = 0,
            Sack = null,
            PuntAttempt = null,
            ReturnYards = null,
            HomeTeamOnOffense = true
        };

        // Act
        await _playHub.SendPlay(playDto);

        // Assert
        _mockPlayClient.Verify(
            client => client.ReceivePlay(It.Is<PlayDto>(p =>
                p.Sack == null &&
                p.PuntAttempt == null &&
                p.ReturnYards == null
            )),
            Times.Once,
            "ReceivePlay should be called with nullable properties set to null"
        );
    }

    [Fact]
    public async Task SendPlay_VerifiesClientsAllPropertyAccessed()
    {
        // Arrange
        var playDto = new PlayDto
        {
            Id = 10,
            GameId = 300,
            HomeTeam = "DAL",
            HomeScore = 14,
            AwayTeam = "NYG",
            AwayScore = 10,
            Quarter = 2,
            QuarterSecondsRemaining = 120,
            GameOver = false,
            Description = "First down"
        };

        // Act
        await _playHub.SendPlay(playDto);

        // Assert
        _mockClients.Verify(
            clients => clients.All,
            Times.Once,
            "Clients.All should be accessed to broadcast to all connected clients"
        );
    }
}
