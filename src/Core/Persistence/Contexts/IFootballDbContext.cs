using Football.Core.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Football.Core.Persistence.Contexts
{
    public interface IFootballDbContext
    {
        public DbSet<PlayEntity> Play { get; }

        public DbSet<GameEntity> Game { get; }

        public DbSet<StatEntity> Stat { get; }

        public DbSet<TimeEntity> Time { get; }
    }
}
