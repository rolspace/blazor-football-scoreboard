using Football.Application.Interfaces;
using Football.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Football.Application.Features.Stats;

public record SaveGameStatsCommand : IRequest<int>
{
    public int GameId { get; init; }

    public int Quarter { get; set; }

    public int QuarterSecondsRemaining { get; set; }

    public bool GameOver { get; set; }

    public List<SaveGameStatsCommandItem> SaveGameStatCommandItems { get; set; } = new List<SaveGameStatsCommandItem>();
}

public class SaveGameStatsCommandItem
{
    public string Team { get; set; } = string.Empty;

    public int Score { get; set; }

    public int PassingYards { get; set; }

    public int Sacks { get; set; }

    public int Punts { get; set; }

    public int ReturnYards { get; set; }
}

public class SaveStatsCommandHandler : IRequestHandler<SaveGameStatsCommand, int>
{
    private readonly IFootballDbContext _footballDbContext;

    public SaveStatsCommandHandler(IFootballDbContext footballDbContext)
    {
        _footballDbContext = footballDbContext;
    }

    public async Task<int> Handle(SaveGameStatsCommand saveGameStatsCommand, CancellationToken cancellationToken)
    {
        Game? game = await _footballDbContext.Games
            .AsQueryable()
            .Where(g => g.Id == saveGameStatsCommand.GameId)
            .SingleOrDefaultAsync();

        if (game is null) return 0;

        foreach (var saveGameStatCommandItem in saveGameStatsCommand.SaveGameStatCommandItems)
        {
            Stat? stat = await _footballDbContext.Stats
                .Where(s => s.GameId == saveGameStatsCommand.GameId && s.Team == saveGameStatCommandItem.Team)
                .SingleOrDefaultAsync();

            if (stat is not null)
            {
                stat.Score = saveGameStatCommandItem.Score;
                stat.PassingYards += saveGameStatCommandItem.PassingYards;
                stat.Sacks += saveGameStatCommandItem.Sacks;
                stat.ReturnYards += saveGameStatCommandItem.ReturnYards;
                stat.Punts += saveGameStatCommandItem.Punts;
            }
            else
            {
                game.Stats.Add(new Stat
                {
                    Team = saveGameStatCommandItem.Team,
                    Score = saveGameStatCommandItem.Score,
                    PassingYards = saveGameStatCommandItem.PassingYards,
                    Sacks = saveGameStatCommandItem.Sacks,
                    ReturnYards = saveGameStatCommandItem.ReturnYards,
                    Punts = saveGameStatCommandItem.Punts
                });
            }
        }


        if (game.State != GameState.Finished && saveGameStatsCommand.GameOver)
        {
            game.State = GameState.Finished;
        }

        if (game.State is null)
        {
            game.State = GameState.Started;
        }

        game.Quarter = saveGameStatsCommand.Quarter;
        game.QuarterSecondsRemaining = saveGameStatsCommand.QuarterSecondsRemaining;

        return await _footballDbContext.SaveChangesAsync(cancellationToken);
    }
}