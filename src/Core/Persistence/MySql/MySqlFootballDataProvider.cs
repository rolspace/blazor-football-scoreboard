using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Football.Core.Models;
using Football.Core.Persistence.Entities;
using Football.Core.Persistence.Interfaces.DataProviders;
using Football.Core.Persistence.MySql.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Football.Core.Persistence.MySql
{
    public class MySqlFootballDataProvider : IFootballDataProvider
    {
        protected readonly FootballDbContext _dbContext;

        public MySqlFootballDataProvider(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Game> GetGame(int gameId)
        {
            GameEntity gameEntity = await _dbContext.Set<GameEntity>()
                .Include(g => g.Time)
                .Include(g => g.Stats)
                .SingleOrDefaultAsync(g => g.Id == gameId);

            return gameEntity.ToModel();
        }

        public async Task<ReadOnlyCollection<Game>> GetGamesByWeek(int week)
        {
            IQueryable<Game> games = _dbContext.Set<GameEntity>()
                .AsQueryable()
                .Where(g => g.Week == week)
                .Select(g => g.ToModel());

            return (await games.ToListAsync()).AsReadOnly();
        }

        public async Task<ReadOnlyCollection<Play>> GetPlaysByWeekAndGameTime(int week, int gameSecondsRemainingStart, int gameSecondsRemainingEnd)
        {
            IQueryable<Play> plays = _dbContext.Set<PlayEntity>()
               .AsQueryable()
               .Where(p => p.Week == week && p.GameSecondsRemaining <= gameSecondsRemainingStart && p.GameSecondsRemaining > gameSecondsRemainingEnd)
               .Select(p => p.ToModel());

            return (await plays.ToListAsync()).AsReadOnly();
        }

        public async Task<ReadOnlyCollection<Stat>> GetGameStats(int gameId)
        {
            IQueryable<Stat> stats = _dbContext.Set<StatEntity>()
                .AsQueryable()
                .Where(s => s.GameId == gameId)
                .Include(s => s.Game.Time)
                .Select(s => s.ToModel());

            return (await stats.ToListAsync()).AsReadOnly();
        }

        public async Task SaveStat(int gameId, string team, PlayLog playLog)
        {
            if (playLog == null) throw new ArgumentNullException("PlayLog should not be null");

            var stat = new Stat
            {
                GameId = gameId,
                Team = team,
                Score = playLog.Score,
                AirYards = playLog.OffensePlayLog?.AirYards ?? 0,
                Sacks = playLog.DefensePlayLog?.Sacks ?? 0,
                Punts = playLog.SpecialPlayLog?.Punts ?? 0,
                ReturnYards = playLog.SpecialPlayLog?.ReturnYards ?? 0
            };

            var shouldAddEntity = false;

            StatEntity statEntity = await _dbContext.Set<StatEntity>()
                .AsQueryable()
                .Where(s => s.GameId == stat.GameId && s.Team == stat.Team)
                .Include(s => s.Game.Time)
                .FirstOrDefaultAsync();

            if (statEntity == null)
            {
                shouldAddEntity = true;

                GameEntity gameEntity = await _dbContext.Set<GameEntity>()
                    .AsQueryable()
                    .Where(g => g.Id == gameId)
                    .Include(g => g.Time)
                    .FirstOrDefaultAsync();

                gameEntity.Time = gameEntity.Time
                    ?? new TimeEntity()
                    {
                        GameId = stat.GameId,
                        Quarter = playLog.Quarter,
                        QuarterSecondsRemaining = playLog.QuarterSecondsRemaining
                    };

                statEntity = new StatEntity
                {
                    GameId = stat.GameId,
                    Team = stat.Team,
                    Game = gameEntity,
                };
            }

            statEntity.Score = stat.Score;
            statEntity.AirYards += stat.AirYards;
            statEntity.ReturnYards += stat.ReturnYards;
            statEntity.Punts += stat.Punts;
            statEntity.Sacks += stat.Sacks;

            if (shouldAddEntity)
            {
                await _dbContext.AddAsync(statEntity);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
