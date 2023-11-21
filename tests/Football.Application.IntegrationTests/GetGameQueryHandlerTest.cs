using AutoMapper;
using FluentAssertions;
using Football.Application.Features.Games;
using Football.Application.Features.Games.Mappings;
using Football.Application.Features.Games.Models;
using Football.Application.IntegrationTests.Fixtures;
using Football.Infrastructure.Persistence;

namespace Football.Application.IntegrationTests;

public class GetGameQueryHandlerTest : IClassFixture<TestDatabaseFixture>
{
    private readonly IMapper _mapper;

    public TestDatabaseFixture Fixture { get; }

    public GetGameQueryHandlerTest(TestDatabaseFixture fixture)
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
    public async Task Handle_GameIdExists_ReturnsGameDto()
    {
        using FootballDbContext dbContext = Fixture.CreateContext();

        GameDto expected = new()
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
        };

        GetGameQueryHandler handler = new(dbContext, _mapper);

        GameDto? result = await handler.Handle(new GetGameQuery
        {
            GameId = 2019090500
        }, new CancellationToken());

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_GameIdNotFound_ReturnsNull()
    {
        using FootballDbContext dbContext = Fixture.CreateContext();

        GetGameQueryHandler handler = new(dbContext, _mapper);

        GameDto? result = await handler.Handle(new GetGameQuery
        {
            GameId = 0
        }, new CancellationToken());

        result.Should().BeNull();
    }
}

