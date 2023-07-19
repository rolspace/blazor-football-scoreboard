using AutoMapper;
using FluentAssertions;
using Football.Application.Features.Stats;
using Football.Application.Features.Stats.Mappings;
using Football.Application.Features.Stats.Models;
using Football.Infrastructure.Persistence;

namespace Football.Application.UnitTests;

public class GetGameStatsQueryTest : IClassFixture<TestDatabaseFixture>
{
    private IMapper _mapper;

    public TestDatabaseFixture Fixture { get; }

    public GetGameStatsQueryTest(TestDatabaseFixture fixture)
    {
        Fixture = fixture;

        if (_mapper is null)
        {
            MapperConfiguration mappingConfig = new(mapperConfiguration =>
            {
                mapperConfiguration.AddProfile(new MapFromEntityProfile());
            });

            _mapper = mappingConfig.CreateMapper();
        }
    }

    [Fact]
    public async Task GetGameStats_GameFound_ReturnsGameStatsDto()
    {
        using FootballDbContext dbContext = Fixture.CreateContext();

        GameStatDto expected = new()
        {
            GameId = 2019090500,
            Stats = new List<StatDto>()
            {
                new StatDto()
                {
                    Team = "CHI",
                    Home = true,
                    Score = 7,
                    PassingYards = 100,
                    Sacks = 4,
                    Punts = 2,
                    ReturnYards = 50
                },
                new StatDto()
                {
                    Team = "GB",
                    Home = false,
                    Score = 7,
                    PassingYards = 50,
                    Sacks = 2,
                    Punts = 1,
                    ReturnYards = 25,
                }
            }
        };

        GetGameStatsQueryHandler handler = new(dbContext, _mapper);

        GameStatDto? result = await handler.Handle(new GetGameStatsQuery
        {
            Id = 2019090500
        }, new CancellationToken());

        result.Should().BeEquivalentTo(expected);
    }
}
