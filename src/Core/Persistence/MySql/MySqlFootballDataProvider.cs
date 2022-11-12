using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Football.Core.Models;
using Football.Core.Persistence.Contexts;
using Football.Core.Persistence.Entities;
using Football.Core.Persistence.Interfaces.DataProviders;
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
                .Include(gameEntity => gameEntity.Time)
                .Include(gameEntity => gameEntity.Stats)
                .Where(gameEntity => gameEntity.Week == week)
                .Select(gameEntity => gameEntity.ToModel());

            return (await games.ToListAsync()).AsReadOnly();
        }

        public async Task<ReadOnlyCollection<Play>> GetPlaysByWeekAndGameTime(int week, int gameSecondsRemainingStart, int gameSecondsRemainingEnd)
        {
            IQueryable<Play> plays = _dbContext.Set<PlayEntity>()
               .AsQueryable()
               .Include(playEntity => playEntity.Game)
               .Where(playEntity => playEntity.Week == week && playEntity.GameSecondsRemaining <= gameSecondsRemainingStart && playEntity.GameSecondsRemaining > gameSecondsRemainingEnd)
               .Select(playEntity => playEntity.ToModel());

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
            if (string.IsNullOrEmpty(team)) throw new ArgumentNullException("Team should not be null or empty");
            if (playLog is null) throw new ArgumentNullException("PlayLog should not be null");

            var shouldAddEntity = false;

            StatEntity statEntity = await _dbContext.Set<StatEntity>()
                .AsQueryable()
                .Where(statEntity => statEntity.GameId == gameId && statEntity.Team == team)
                .Include(statEntity => statEntity.Game.Time)
                .FirstOrDefaultAsync();

            if (statEntity is not null)
            {
                statEntity.Game.Time.Quarter = playLog.Quarter;
                statEntity.Game.Time.QuarterSecondsRemaining = playLog.QuarterSecondsRemaining;
            }
            else if (statEntity is null)
            {
                shouldAddEntity = true;

                GameEntity gameEntity = await _dbContext.Set<GameEntity>()
                    .AsQueryable()
                    .Where(gameEntity => gameEntity.Id == gameId)
                    .Include(gameEntity => gameEntity.Time)
                    .FirstOrDefaultAsync();

                if (gameEntity is null) throw new Exception("Game must exist in order to save statistics");

                gameEntity.Time = gameEntity.Time ?? new TimeEntity()
                {
                    GameId = gameId
                };

                gameEntity.Time.Quarter = playLog.Quarter;
                gameEntity.Time.QuarterSecondsRemaining = playLog.QuarterSecondsRemaining;

                statEntity = new StatEntity
                {
                    GameId = gameId,
                    Team = team,
                    Game = gameEntity,
                };
            }

            statEntity.Score = playLog.Score;
            statEntity.AirYards += playLog.OffensePlayLog?.AirYards ?? 0;
            statEntity.ReturnYards += playLog.SpecialPlayLog?.ReturnYards ?? 0;
            statEntity.Punts += playLog.SpecialPlayLog?.Punts ?? 0;
            statEntity.Sacks += playLog.DefensePlayLog?.Sacks ?? 0;

            if (shouldAddEntity)
            {
                await _dbContext.AddAsync(statEntity);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
