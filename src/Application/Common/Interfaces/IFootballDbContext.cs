using Football.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Football.Application.Common.Interfaces;

public interface IFootballDbContext
{
    DbSet<Game> Games { get; }

    DbSet<Play> Plays { get; }

    DbSet<Stat> Stats { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}