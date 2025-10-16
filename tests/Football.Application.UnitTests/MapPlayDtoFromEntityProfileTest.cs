using AutoMapper;
using AutoMapper.QueryableExtensions;
using Football.Application.Features.Plays.Mappings;
using Football.Application.Features.Plays.Models;
using Football.Domain.Entities;

namespace Football.Application.UnitTests;

public class MapPlayDtoFromEntityProfileTest
{
    private readonly IMapper _mapper;

    public MapPlayDtoFromEntityProfileTest()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapFromEntityProfile>();
        });
        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void MapFromEntityProfile_Play_MapsToPlayDto()
    {
        // Arrange
        var play = new Play
        {
            PlayId = 123,
            GameId = 456,
            HomeTeam = "Chiefs",
            HomeScore = 21,
            AwayTeam = "Bills",
            AwayScore = 14,
            Qtr = 3,
            QuarterSecondsRemaining = 540,
            Desc = "Pass complete to T.Kelce for 15 yards",
            PlayType = "pass",
            Posteam = "Chiefs",
            YardsGained = 15,
            Sack = false
        };

        IQueryable<Play> plays = new List<Play> { play }.AsQueryable();

        // Act
        PlayDto result = plays.ProjectTo<PlayDto>(_mapper.ConfigurationProvider).First();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(123, result.Id);
        Assert.Equal(456, result.GameId);
        Assert.Equal(21, result.HomeScore);
        Assert.Equal(14, result.AwayScore);
        Assert.Equal(3, result.Quarter);
        Assert.False(result.GameOver);
        Assert.Equal("Pass complete to T.Kelce for 15 yards", result.Description);
        Assert.False(result.Kickoff);
        Assert.False(result.Punt);
        Assert.True(result.HomeTeamPossession);
        Assert.True(result.HomeTeamOnOffense);
        Assert.False(result.AwayTeamOnOffense);
    }

    [Fact]
    public void MapFromEntityProfile_Play_WithEndGame_MapsGameOverCorrectly()
    {
        // Arrange
        var play = new Play
        {
            PlayId = 999,
            GameId = 456,
            HomeTeam = "Chiefs",
            HomeScore = 28,
            AwayTeam = "Bills",
            AwayScore = 24,
            Qtr = 4,
            Desc = "END GAME",
            PlayType = "pass",
            Posteam = "Chiefs"
        };

        IQueryable<Play> plays = new List<Play> { play }.AsQueryable();

        // Act
        PlayDto result = plays.ProjectTo<PlayDto>(_mapper.ConfigurationProvider).First();

        // Assert
        Assert.True(result.GameOver);
        Assert.Equal("END GAME", result.Description);
    }

    [Fact]
    public void MapFromEntityProfile_Play_WithKickoff_MapsKickoffCorrectly()
    {
        // Arrange
        var play = new Play
        {
            PlayId = 1,
            GameId = 456,
            HomeTeam = "Chiefs",
            HomeScore = 0,
            AwayTeam = "Bills",
            AwayScore = 0,
            Qtr = 1,
            Desc = "Chiefs kickoff to Bills",
            PlayType = "kickoff",
            Posteam = "Bills"
        };

        IQueryable<Play> plays = new List<Play> { play }.AsQueryable();

        // Act
        PlayDto result = plays.ProjectTo<PlayDto>(_mapper.ConfigurationProvider).First();

        // Assert
        Assert.True(result.Kickoff);
        Assert.False(result.Punt);
        Assert.False(result.HomeTeamPossession);
        Assert.False(result.HomeTeamOnOffense);
        Assert.True(result.AwayTeamOnOffense);
    }

    [Fact]
    public void MapFromEntityProfile_Play_WithPunt_MapsPuntCorrectly()
    {
        // Arrange
        var play = new Play
        {
            PlayId = 50,
            GameId = 456,
            HomeTeam = "Chiefs",
            HomeScore = 7,
            AwayTeam = "Bills",
            AwayScore = 10,
            Qtr = 2,
            Desc = "Bills punt to Chiefs",
            PlayType = "punt",
            Posteam = "Bills"
        };

        IQueryable<Play> plays = new List<Play> { play }.AsQueryable();

        // Act
        PlayDto result = plays.ProjectTo<PlayDto>(_mapper.ConfigurationProvider).First();

        // Assert
        Assert.False(result.Kickoff);
        Assert.True(result.Punt);
        Assert.False(result.HomeTeamPossession);
        Assert.False(result.HomeTeamOnOffense);
        Assert.True(result.AwayTeamOnOffense);
    }

    [Fact]
    public void MapFromEntityProfile_Play_WithAwayTeamPossession_MapsTeamPossessionCorrectly()
    {
        // Arrange
        var play = new Play
        {
            PlayId = 75,
            GameId = 456,
            HomeTeam = "Chiefs",
            HomeScore = 14,
            AwayTeam = "Bills",
            AwayScore = 17,
            Qtr = 3,
            Desc = "Rush for 5 yards",
            PlayType = "run",
            Posteam = "Bills"
        };

        IQueryable<Play> plays = new List<Play> { play }.AsQueryable();

        // Act
        PlayDto result = plays.ProjectTo<PlayDto>(_mapper.ConfigurationProvider).First();

        // Assert
        Assert.False(result.HomeTeamPossession);
        Assert.False(result.HomeTeamOnOffense);
        Assert.True(result.AwayTeamOnOffense);
    }
}
