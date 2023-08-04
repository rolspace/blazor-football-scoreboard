using AutoMapper;
using FluentAssertions;
using Football.Application.Features.Stats;
using Football.Application.Features.Stats.Mappings;
using Football.Application.IntegrationTests.Fixtures;
using Football.Domain.Entities;
using Football.Infrastructure.Persistence;

namespace Football.Application.IntegrationTests;

public class SaveGameStatsCommandHandlerTest : IClassFixture<TestDatabaseFixture>
{
    private readonly IMapper _mapper;

    public TestDatabaseFixture Fixture { get; }

    public SaveGameStatsCommandHandlerTest(TestDatabaseFixture fixture)
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
    public async Task Handle_ValidGameStats_SaveSuccessful()
    {
        using FootballDbContext dbContext = Fixture.CreateContext();

        await dbContext.Database.BeginTransactionAsync();

        SaveGameStatsCommand saveGameStatsCommand = new()
        {
            GameId = 2019090500,
            Quarter = 1,
            QuarterSecondsRemaining = 850,
            GameOver = false,
            SaveGameStatCommandItems = new List<SaveGameStatsCommandItem>()
            {
                new SaveGameStatsCommandItem()
                {
                    Team = "CHI",
                    Score = 14,
                    PassingYards = 20,
                    Sacks = 0,
                    Punts = 0,
                    ReturnYards = 0,
                },
                new SaveGameStatsCommandItem()
                {
                    Team = "GB",
                    Score = 7,
                    PassingYards = 0,
                    Sacks = 0,
                    Punts = 0,
                    ReturnYards = 0
                }
            }
        };

        SaveGameStatsCommandHandler handler = new(dbContext);
        await handler.Handle(saveGameStatsCommand, new CancellationToken());

        dbContext.ChangeTracker.Clear();

        // Test should get data directly from the db
        Stat stat = dbContext.Stats.AsQueryable().Single(s => s.GameId == saveGameStatsCommand.GameId && s.Team == "CHI");
        stat.Score.Should().Be(14);
    }
}
