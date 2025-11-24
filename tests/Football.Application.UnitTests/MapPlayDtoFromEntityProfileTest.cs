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
            Week = 10,
            HomeTeam = "Chiefs",
            HomeScore = 21,
            AwayTeam = "Bills",
            AwayScore = 14,
            Quarter = 3,
            QuarterSecondsRemaining = 540,
            HalfSecondsRemaining = 1740,
            GameSecondsRemaining = 2340,
            GameDate = "2024-11-10",
            GameHalf = "Half2",
            Description = "Pass complete to T.Kelce for 15 yards",
            PlayTime = "9:00",
            SideOfField = "KC",
            YardLine = "KC 35",
            PlayType = "pass",
            Down = 2,
            GoalToGo = 0,
            Yardline100 = 65,
            Drive = 5,
            YardsToGo = 7,
            QuarterEnd = false,
            Touchdown = false,
            Shotgun = true,
            NoHuddle = false,
            QbDropback = true,
            QbKneel = false,
            QbSpike = false,
            QbScramble = false,
            QbHit = false,
            FirstDownPass = true,
            FirstDownRush = false,
            FirstDownPenalty = false,
            ThirdDownConverted = false,
            ThirdDownFailed = false,
            FourthDownConverted = false,
            FourthDownFailed = false,
            PassAttempt = true,
            PassTouchdown = false,
            CompletePass = true,
            IncompletePass = false,
            Interception = false,
            RushAttempt = false,
            RushTouchdown = false,
            Fumble = false,
            FumbleForced = false,
            FumbleNotForced = false,
            FumbleLost = false,
            FumbleOutOfBounds = false,
            Touchback = false,
            ReturnTouchdown = false,
            PuntAttempt = false,
            PuntBlocked = false,
            KickoffAttempt = false,
            OwnKickoffRecovery = false,
            OwnKickoffRecoveryTd = false,
            ExtraPointAttempt = false,
            TwoPointAttempt = false,
            FieldGoalAttempt = false,
            SoloTackle = false,
            AssistTackle = false,
            TackleForLoss = false,
            Sack = false,
            Safety = false,
            Penalty = false,
            ReplayOrChallenge = false,
            DefensiveTwoPointAttempt = false,
            DefensiveTwoPointConv = false,
            DefensiveExtraPointAttempt = false,
            DefensiveExtraPointConv = false,
            Posteam = "Chiefs",
            PosteamType = "home",
            PosteamScore = 21,
            Defteam = "Bills",
            DefteamScore = 14,
            TdTeam = null,
            NetYards = 15,
            YardsGained = 15,
            PassLength = "short",
            PassLocation = "middle",
            AirYards = 10,
            YardsAfterCatch = 5,
            RunLocation = null,
            RunGap = null,
            FieldGoalResult = null,
            ExtraPointResult = null,
            TwoPointConvResult = null,
            ReturnTeam = null,
            ReturnYards = null,
            KickDistance = null,
            PenaltyTeam = null,
            PenaltyType = null,
            PenaltyYards = null,
            ReplayOrChallengeResult = null,
            Timeout = false,
            TimeoutTeam = null,
            HomeTimeoutsRemaining = 3,
            AwayTimeoutsRemaining = 2,
            PosteamTimeoutsRemaining = 3,
            DefteamTimeoutsRemaining = 2,
        };

        IQueryable<Play> plays = new List<Play> { play }.AsQueryable();

        // Act
        PlayDto result = plays.ProjectTo<PlayDto>(_mapper.ConfigurationProvider).First();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(123, result.Id);
        Assert.Equal(456, result.GameId);
        Assert.Equal("Chiefs", result.HomeTeam);
        Assert.Equal(21, result.HomeScore);
        Assert.Equal("Bills", result.AwayTeam);
        Assert.Equal(14, result.AwayScore);
        Assert.Equal(3, result.Quarter);
        Assert.Equal(540, result.QuarterSecondsRemaining);
        Assert.Equal("09:00", result.QuarterTimeRemaining);
        Assert.False(result.GameOver);
        Assert.Equal("Pass complete to T.Kelce for 15 yards", result.Description);
        Assert.Equal("pass", result.PlayType);
        Assert.Equal(15, result.YardsGained);
        Assert.False(result.Sack);
        Assert.False(result.Kickoff);
        Assert.False(result.PuntAttempt);
        Assert.False(result.Punt);
        Assert.Null(result.ReturnYards);
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
            Quarter = 4,
            Description = "END GAME",
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
            Quarter = 1,
            Description = "Chiefs kickoff to Bills",
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
            Quarter = 2,
            Description = "Bills punt to Chiefs",
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
            Quarter = 3,
            Description = "Rush for 5 yards",
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
