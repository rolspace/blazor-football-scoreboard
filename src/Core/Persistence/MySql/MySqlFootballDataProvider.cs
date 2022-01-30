using Football.Core.Models;
using Football.Core.Persistence.Entities;
using Football.Core.Persistence.Interfaces.DataProviders;
using Football.Core.Persistence.MySql.Contexts;
using Football.Core.Persistence.MySql.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Football.Core.Persistence.MySql
{
    public class MySqlFootballDataProvider : IFootballDataProvider
    {
        protected readonly FootballDbContext _dbContext;

        public MySqlFootballDataProvider(FootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ReadOnlyCollection<Game>> GetGamesByWeek(int week)
        {
            IQueryable<Game> games = _dbContext.Set<GameEntity>()
                .AsQueryable()
                .Where(g => g.Week == week)
                .Select(g => EntityMapper.MapToGameModel(g));

            return (await games.ToListAsync()).AsReadOnly();
        }

        public async Task<ReadOnlyCollection<Play>> GetPlaysByWeekAndGameTime(int week, int gameSecondsRemainingStart, int gameSecondsRemainingEnd)
        {
             IQueryable<Play> plays = _dbContext.Set<PlayEntity>()
                .AsQueryable()
                .Where(p => p.Week == week && p.GameSecondsRemaining <= gameSecondsRemainingStart && p.GameSecondsRemaining > gameSecondsRemainingEnd)
                .Select(p => EntityMapper.MapToPlayModel(p));

            return (await plays.ToListAsync()).AsReadOnly();
        }

        public async Task<ReadOnlyCollection<Stat>> GetGameStats(int gameId)
        {
            IQueryable<Stat> stats = _dbContext.Set<StatEntity>()
                .AsQueryable()
                .Where(s => s.GameId == gameId)
                .Select(s => EntityMapper.MapToStatModel(s));

            return (await stats.ToListAsync()).AsReadOnly();
        }

        public async Task SaveStat(Stat stat)
        {
            var shouldAddEntity = false;

            StatEntity statEntity = await _dbContext.Set<StatEntity>()
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.GameId == stat.GameId && s.Team == stat.Team);

            TimeEntity timeEntity = await _dbContext.Set<TimeEntity>()
                .AsQueryable()
                .FirstOrDefaultAsync(t => t.GameId == stat.GameId);

            if (statEntity == null)
            {
                shouldAddEntity = true;

                statEntity = new StatEntity
                {
                    GameId = stat.GameId,
                    Team = stat.Team,
                    Time = timeEntity ?? new TimeEntity()
                };
            }

            statEntity.Score = stat.Score;
            statEntity.AirYards += stat.AirYards;
            statEntity.ReturnYards += stat.ReturnYards;
            statEntity.Punts += stat.Punts;
            statEntity.Sacks += stat.Sacks;
            statEntity.Time.GameId = stat.GameId;
            statEntity.Time.Quarter = stat.Quarter;
            statEntity.Time.QuarterSecondsRemaining = stat.QuarterSecondsRemaining;

            if (shouldAddEntity)
            {
                await _dbContext.AddAsync(statEntity);
            }
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
