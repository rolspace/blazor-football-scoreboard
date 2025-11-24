using System.Reflection;
using Football.Application.Interfaces;
using Football.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Football.Infrastructure.Persistence;

public class FootballDbContext : DbContext, IFootballDbContext
{
    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Play> Plays { get; set; }

    public virtual DbSet<Stat> Stats { get; set; }

    public FootballDbContext(DbContextOptions<FootballDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
