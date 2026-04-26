using FluentAssertions;
using Football.Application.Features.Stats;
using Football.Domain.Entities;
using Football.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Football.Api.IntegrationTests;

[Collection(ApiIntegrationTestCollection.Name)]
public class SaveGameStatsCommandTests
{
    private readonly IServiceProvider _services;

    public SaveGameStatsCommandTests(ApiIntegrationTestFixture fixture)
    {
        _services = fixture.Services;
    }

    [Fact]
    public async Task SaveGameStats_ValidGameStats_SaveSuccessful()
    {
        using IServiceScope scope = _services.CreateScope();
        ISender mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        FootballDbContext dbContext = scope.ServiceProvider.GetRequiredService<FootballDbContext>();

        await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

        SaveGameStatsCommand command = new()
        {
            GameId = 2019090500,
            Quarter = 1,
            QuarterSecondsRemaining = 850,
            GameOver = false,
            SaveGameStatCommandItems = new List<SaveGameStatsCommandItem>()
            {
                new()
                {
                    Team = "CHI",
                    Score = 14,
                    PassingYards = 20,
                    Sacks = 0,
                    Punts = 0,
                    ReturnYards = 0,
                },
                new()
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

        await mediator.Send(command);

        dbContext.ChangeTracker.Clear();

        Stat stat = await dbContext.Stats
            .AsQueryable()
            .SingleAsync(s => s.GameId == command.GameId && s.Team == "CHI");

        stat.Score.Should().Be(14);

        await transaction.RollbackAsync();
    }
}
