using Core.Infrastructure.Models;
using Core.Infrastructure.MySql.Contexts;
using Core.Infrastructure.MySql.Entities;
using Core.Infrastructure.MySql.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        protected readonly FootballDbContext _dbContext;

        public Repository()
            : this(new FootballDbContext()) { }

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

        public async Task<ReadOnlyCollection<IPlay>> GetPlaysByGameTime(int gameSecondsRemainingStart, int gameSecondsRemainingEnd)
        {
             IQueryable<IPlay> plays = _dbContext.Set<PlayEntity>()
                .AsQueryable()
                .Where(p => p.GameSecondsRemaining <= gameSecondsRemainingStart && p.GameSecondsRemaining > gameSecondsRemainingEnd)
                .Select(p => ModelMapper.MapPlayModel(p));

            return (await plays.ToListAsync()).AsReadOnly();
        }
    }
}
