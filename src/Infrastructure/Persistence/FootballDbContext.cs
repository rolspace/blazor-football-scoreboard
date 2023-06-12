using System.Reflection;
using Football.Application.Interfaces;
using Football.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Football.Infrastructure.Persistence;

public class FootballDbContext : DbContext, IFootballDbContext
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Play> Plays => Set<Play>();

    public DbSet<Stat> Stats => Set<Stat>();

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
