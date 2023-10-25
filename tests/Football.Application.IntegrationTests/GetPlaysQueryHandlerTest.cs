using AutoMapper;
using FluentAssertions;
using Football.Application.Features.Plays;
using Football.Application.Features.Plays.Mappings;
using Football.Application.Features.Plays.Models;
using Football.Application.IntegrationTests.Fixtures;
using Football.Infrastructure.Persistence;

namespace Football.Application.IntegrationTests;

public class GetPlaysQueryHandlerTest : IClassFixture<TestDatabaseFixture>
{
    private readonly IMapper _mapper;

    public TestDatabaseFixture Fixture { get; }

    public GetPlaysQueryHandlerTest(TestDatabaseFixture fixture)
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
    public async Task Handle_PlaysFoundForSearchParams_ReturnsPlayDtoCollection()
    {
        using FootballDbContext dbContext = Fixture.CreateContext();

        var expected = new List<PlayDto>()
        {
            new()
            {
                Id = 35,
                GameId = 2019090500,
                HomeTeam = "CHI",
                HomeScore = 0,
                AwayTeam = "GB",
                AwayScore = 0,
                Description = "E.Pineiro kicks 65 yards from CHI 35 to end zone, Touchback.",
                Quarter = 1,
                QuarterSecondsRemaining = 900,
                GameOver = false,
                PlayType = "kickoff",
                YardsGained = 0,
                Sack = false,
                Kickoff = true,
                PuntAttempt = false,
                ReturnYards = 0,
                HomeTeamPossession =  false,
                HomeTeamOnOffense = false,
                AwayTeamOnOffense = true,
            },
            new()
            {
                Id = 50,
                GameId = 2019090500,
                HomeTeam = "CHI",
                HomeScore = 0,
                AwayTeam = "GB",
                AwayScore = 0,
                Description = "(15:00) A.Jones left tackle to GB 25 for no gain (R.Smith).",
                Quarter = 1,
                QuarterSecondsRemaining = 900,
                GameOver = false,
                PlayType = "run",
                YardsGained = 0,
                Sack = false,
                Kickoff = false,
                PuntAttempt = false,
                ReturnYards = 0,
                HomeTeamPossession =  false,
                HomeTeamOnOffense = false,
                AwayTeamOnOffense = true,
            }
        };

        GetPlaysQueryHandler handler = new(dbContext, _mapper);

        IEnumerable<PlayDto> result = await handler.Handle(new GetPlaysQuery
        {
            Week = 1,
            Quarter = 1,
            QuarterSecondsRemaining = 900
        }, new CancellationToken());

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_PlaysNotFoundForSearchParams_ReturnsEmptyCollection()
    {
        using FootballDbContext dbContext = Fixture.CreateContext();

        GetPlaysQueryHandler handler = new(dbContext, _mapper);

        IEnumerable<PlayDto> result = await handler.Handle(new GetPlaysQuery
        {
            Week = 1,
            Quarter = 1,
            QuarterSecondsRemaining = 1000
        }, new CancellationToken());

        result.Should().BeEquivalentTo(Enumerable.Empty<PlayDto>());
    }
}
