using AutoMapper;
using FluentAssertions;
using Football.Application.Features.Games;
using Football.Application.Features.Games.Mappings;
using Football.Application.Features.Games.Models;
using Football.Application.IntegrationTests.Fixtures;
using Football.Infrastructure.Persistence;

namespace Football.Application.IntegrationTests;

public class GetGamesQueryHandlerTest : IClassFixture<TestDatabaseFixture>
{
    private readonly IMapper _mapper;

    public TestDatabaseFixture Fixture { get; }

    public GetGamesQueryHandlerTest(TestDatabaseFixture fixture)
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
    public async Task Handle_GamesFoundForWeek_ReturnsGameDtoCollection()
    {
        using FootballDbContext dbContext = Fixture.CreateContext();

        var expected = new List<GameDto>()
        {
            new GameDto()
            {
                Id = 2019090500,
                Week = 1,
                HomeTeam = "CHI",
                HomeScore = 7,
                AwayTeam = "GB",
                AwayScore = 7,
                State = string.Empty,
                Quarter = 1,
                QuarterSecondsRemaining = 900
            }
        };

        GetGamesQueryHandler handler = new(dbContext, _mapper);

        IEnumerable<GameDto> result = await handler.Handle(new GetGamesQuery
        {
            Week = 1
        }, new CancellationToken());

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_GamesNotFoundForWeek_ReturnsEmptyCollection()
    {
        using FootballDbContext dbContext = Fixture.CreateContext();

        GetGamesQueryHandler handler = new(dbContext, _mapper);

        IEnumerable<GameDto> result = await handler.Handle(new GetGamesQuery
        {
            Week = 0
        }, new CancellationToken());

        result.Should().BeEmpty();
    }
}
