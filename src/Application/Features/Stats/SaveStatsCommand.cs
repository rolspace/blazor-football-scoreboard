using Football.Application.Interfaces;
using Football.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Football.Application.Features.Stats;

public record SaveStatsCommand : IRequest<int>
{
    public int GameId { get; init; }

    public int Quarter { get; set; }

    public int QuarterSecondsRemaining { get; set; }

    public bool GameOver { get; set; }

    public List<SaveStatsCommandItem> SaveStatCommandItems { get; set; } = new List<SaveStatsCommandItem>();
}

public class SaveStatsCommandItem
{
    public string Team { get; set; } = string.Empty;

    public int Score { get; set; }

    public int PassingYards { get; set; }

    public int Sacks { get; set; }

    public int Punts { get; set; }

    public int ReturnYards { get; set; }
}

public class SaveStatsCommandHandler : IRequestHandler<SaveStatsCommand, int>
{
    private readonly IFootballDbContext _footballDbContext;

    public SaveStatsCommandHandler(IFootballDbContext footballDbContext)
    {
        _footballDbContext = footballDbContext;
    }

    public async Task<int> Handle(SaveStatsCommand saveStatsCommand, CancellationToken cancellationToken)
    {
        Game? game = await _footballDbContext.Games
            .AsQueryable()
            .Where(g => g.Id == saveStatsCommand.GameId)
            .SingleOrDefaultAsync();

        if (game is null) return 0;

        foreach (var saveStatCommandItem in saveStatsCommand.SaveStatCommandItems)
        {
            Stat? stat = await _footballDbContext.Stats
                .Where(s => s.GameId == saveStatsCommand.GameId && s.Team == saveStatCommandItem.Team)
                .SingleOrDefaultAsync();

            if (stat is not null)
            {
                stat.Score = saveStatCommandItem.Score;
                stat.PassingYards += saveStatCommandItem.PassingYards;
                stat.Sacks += saveStatCommandItem.Sacks;
                stat.ReturnYards += saveStatCommandItem.ReturnYards;
                stat.Punts += saveStatCommandItem.Punts;
            }
            else
            {
                game.Stats.Add(new Stat
                {
                    Team = saveStatCommandItem.Team,
                    Score = saveStatCommandItem.Score,
                    PassingYards = saveStatCommandItem.PassingYards,
                    Sacks = saveStatCommandItem.Sacks,
                    ReturnYards = saveStatCommandItem.ReturnYards,
                    Punts = saveStatCommandItem.Punts
                });
            }
        }


        if (game.State != GameState.Finished && saveStatsCommand.GameOver)
        {
            game.State = GameState.Finished;
        }

        if (game.State is null)
        {
            game.State = GameState.Started;
        }

        game.Quarter = saveStatsCommand.Quarter;
        game.QuarterSecondsRemaining = saveStatsCommand.QuarterSecondsRemaining;

        return await _footballDbContext.SaveChangesAsync(cancellationToken);
    }
}
