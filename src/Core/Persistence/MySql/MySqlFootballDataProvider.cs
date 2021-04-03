using Football.Core.Interfaces.Models;
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

        public async Task<ReadOnlyCollection<IGame>> GetGamesByWeek(int week)
        {
            IQueryable<IGame> games = _dbContext.Set<GameEntity>()
                .AsQueryable()
                .Where(g => g.Week == week)
                .Select(g => ModelMapper.MapGameModel(g));

            return (await games.ToListAsync()).AsReadOnly();
        }

        public async Task<ReadOnlyCollection<IPlay>> GetPlaysByWeekAndGameTime(int week, int gameSecondsRemainingStart, int gameSecondsRemainingEnd)
        {
             IQueryable<IPlay> plays = _dbContext.Set<PlayEntity>()
                .AsQueryable()
                .Where(p => p.Week == week && p.GameSecondsRemaining <= gameSecondsRemainingStart && p.GameSecondsRemaining > gameSecondsRemainingEnd)
                .Select(p => ModelMapper.MapPlayModel(p));;

            return (await plays.ToListAsync()).AsReadOnly();
        }

        public async Task SaveStat(IStat stat)
        {
            StatEntity statEntity = await _dbContext.Set<StatEntity>()
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.GameId == stat.GameId && s.Team == stat.Team);

            if (statEntity == null)
            {
                statEntity = new StatEntity();
                statEntity.GameId = stat.GameId;
                statEntity.Team = stat.Team;
            }

            statEntity.AirYards += stat.AirYards;

            _dbContext.Update(statEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
