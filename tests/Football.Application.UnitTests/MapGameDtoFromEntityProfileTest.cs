using AutoMapper;
using AutoMapper.QueryableExtensions;
using Football.Application.Features.Games.Mappings;
using Football.Application.Features.Games.Models;
using Football.Domain.Entities;
using Football.Domain.Enums;

namespace Football.Application.UnitTests;

public class MapGameDtoFromEntityProfileTest
{
    private readonly IMapper _mapper;

    public MapGameDtoFromEntityProfileTest()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapFromEntityProfile>();
        });
        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void MapFromEntityProfile_Game_MapsToGameDto()
    {
        // Arrange
        var game = new Game
        {
            Id = 1,
            Week = 5,
            HomeTeam = "Chiefs",
            AwayTeam = "Bills",
            State = GameState.Started,
            Quarter = 2,
            QuarterSecondsRemaining = 600
        };

        game.Stats.Add(new Stat { Team = "Chiefs", Score = 21 });
        game.Stats.Add(new Stat { Team = "Bills", Score = 14 });

        IQueryable<Game> games = new List<Game> { game }.AsQueryable();

        // Act
        GameDto result = games.ProjectTo<GameDto>(_mapper.ConfigurationProvider).First();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(5, result.Week);
        Assert.Equal("Chiefs", result.HomeTeam);
        Assert.Equal("Bills", result.AwayTeam);
        Assert.Equal(21, result.HomeScore);
        Assert.Equal(14, result.AwayScore);
        Assert.Equal(2, result.Quarter);
        Assert.Equal(600, result.QuarterSecondsRemaining);
    }
}
