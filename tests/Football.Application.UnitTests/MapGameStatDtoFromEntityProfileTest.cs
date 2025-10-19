using AutoMapper;
using AutoMapper.QueryableExtensions;
using Football.Application.Features.Stats.Mappings;
using Football.Application.Features.Stats.Models;
using Football.Domain.Entities;

namespace Football.Application.UnitTests;

public class MapGameStatDtoFromEntityProfileTest
{
    private readonly IMapper _mapper;

    public MapGameStatDtoFromEntityProfileTest()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapFromEntityProfile>();
        });
        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void MapFromEntityProfile_StatsForHomeTeam_MapsToStatDto()
    {
        // Arrange
        var game = new Game
        {
            Id = 1,
            HomeTeam = "Chiefs",
            AwayTeam = "Bills"
        };

        var stat = new Stat
        {
            GameId = 1,
            Game = game,
            Team = "Chiefs",
            Score = 21,
            PassingYards = 350,
            Sacks = 2,
            Punts = 3,
            ReturnYards = 75
        };

        IQueryable<Stat> stats = new List<Stat> { stat }.AsQueryable();

        // Act
        StatDto result = stats.ProjectTo<StatDto>(_mapper.ConfigurationProvider).First();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Chiefs", result.Team);
        Assert.Equal(21, result.Score);
        Assert.True(result.Home);
        Assert.Equal(350, result.PassingYards);
        Assert.Equal(2, result.Sacks);
        Assert.Equal(3, result.Punts);
        Assert.Equal(75, result.ReturnYards);
    }

    [Fact]
    public void MapFromEntityProfile_StatsForVisitingTeam_MapsToStatDto()
    {
        // Arrange
        var game = new Game
        {
            Id = 1,
            HomeTeam = "Chiefs",
            AwayTeam = "Bills"
        };

        var stat = new Stat
        {
            GameId = 1,
            Game = game,
            Team = "Bills",
            Score = 14,
            PassingYards = 280,
            Sacks = 1,
            Punts = 5,
            ReturnYards = 50
        };

        IQueryable<Stat> stats = new List<Stat> { stat }.AsQueryable();

        // Act
        StatDto result = stats.ProjectTo<StatDto>(_mapper.ConfigurationProvider).First();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Bills", result.Team);
        Assert.Equal(14, result.Score);
        Assert.False(result.Home);
        Assert.Equal(280, result.PassingYards);
        Assert.Equal(1, result.Sacks);
        Assert.Equal(5, result.Punts);
        Assert.Equal(50, result.ReturnYards);
    }

    [Fact]
    public void MapFromEntityProfile_Stat_MapsToStatDto()
    {
        // Arrange
        var stat = new Stat
        {
            GameId = 1,
            Game = null,
            Team = "Chiefs",
            Score = 21,
            PassingYards = 350,
            Sacks = 2,
            Punts = 3,
            ReturnYards = 75
        };

        IQueryable<Stat> stats = new List<Stat> { stat }.AsQueryable();

        // Act
        StatDto result = stats.ProjectTo<StatDto>(_mapper.ConfigurationProvider).First();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Chiefs", result.Team);
        Assert.Equal(21, result.Score);
        Assert.False(result.Home); // Should be false when Game is null
        Assert.Equal(350, result.PassingYards);
        Assert.Equal(2, result.Sacks);
        Assert.Equal(3, result.Punts);
        Assert.Equal(75, result.ReturnYards);
    }
}
