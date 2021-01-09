using Football.Core.Interfaces;
using Football.Core.Persistence.MySql.Contexts;
using Football.Core.Persistence.MySql.Entities;
using Football.Core.Persistence.MySql.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Football.Core.Persistence.MySql
{
    public class Repository : IRepository
    {
        protected readonly FootballDbContext _dbContext;

        public Repository(FootballDbContext dbContext)
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

        public async Task<ReadOnlyCollection<IPlay>> GetPlaysByGameTime(int week, int gameSecondsRemainingStart, int gameSecondsRemainingEnd)
        {
             IQueryable<IPlay> plays = _dbContext.Set<PlayEntity>()
                .AsQueryable()
                .Where(p => p.Week == week && p.GameSecondsRemaining <= gameSecondsRemainingStart && p.GameSecondsRemaining > gameSecondsRemainingEnd)
                .Select(p => ModelMapper.MapPlayModel(p));;

            return (await plays.ToListAsync()).AsReadOnly();
        }
    }
}
