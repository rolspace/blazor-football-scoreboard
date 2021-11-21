using Football.Core.Models;
using Football.Core.Persistence.Interfaces.DataProviders;
using Football.Core.Persistence.MySql.Contexts;
using Football.Core.Persistence.MySql.Entities;
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
                .Select(g => ModelMapper.MapGameModel(g));

            return (await games.ToListAsync()).AsReadOnly();
        }

        public async Task<ReadOnlyCollection<Play>> GetPlaysByWeekAndGameTime(int week, int gameSecondsRemainingStart, int gameSecondsRemainingEnd)
        {
             IQueryable<Play> plays = _dbContext.Set<PlayEntity>()
                .AsQueryable()
                .Where(p => p.Week == week && p.GameSecondsRemaining <= gameSecondsRemainingStart && p.GameSecondsRemaining > gameSecondsRemainingEnd)
                .Select(p => ModelMapper.MapPlayModel(p));

            return (await plays.ToListAsync()).AsReadOnly();
        }

        public async Task<ReadOnlyCollection<Stat>> GetGameStats(int gameId)
        {
            IQueryable<Stat> stats = _dbContext.Set<StatEntity>()
                .AsQueryable()
                .Where(s => s.GameId == gameId)
                .Select(s => ModelMapper.MapStatModel(s));

            return (await stats.ToListAsync()).AsReadOnly();
        }

        public async Task SaveStat(Stat stat)
        {
            var isNew = false;
            StatEntity statEntity = await _dbContext.Set<StatEntity>()
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.GameId == stat.GameId && s.Team == stat.Team);

            if (statEntity == null)
            {
                isNew = true;

                statEntity = new StatEntity();
                statEntity.GameId = stat.GameId;
                statEntity.Team = stat.Team;
            }

            statEntity.Score = stat.Score;
            statEntity.AirYards += stat.AirYards;
            statEntity.ReturnYards += stat.ReturnYards;
            statEntity.Punts += stat.Punts;
            statEntity.Sacks += stat.Sacks;

            if (isNew)
            {
                await _dbContext.AddAsync(statEntity);
            }
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
