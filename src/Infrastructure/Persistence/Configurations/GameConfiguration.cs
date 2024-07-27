using Football.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football.Infrastructure.Persistence.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("game");

        entityTypeBuilder
            .HasKey(e => e.Id)
            .HasName("PRIMARY");

        entityTypeBuilder
            .Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        entityTypeBuilder
            .Property(e => e.Week)
            .HasColumnName("week");

        entityTypeBuilder
            .HasIndex(g => new { g.Week })
            .HasDatabaseName("IX_Game_Week");

        entityTypeBuilder
            .Property(e => e.HomeTeam)
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnName("home_team");

        entityTypeBuilder
            .Property(e => e.AwayTeam)
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnName("away_team");

        // TODO: check how to represent status table
        entityTypeBuilder
            .Property(e => e.State)
            .HasConversion<string>()
            .HasDefaultValue(null)
            .HasMaxLength(10)
            .HasColumnName("state");

        entityTypeBuilder
            .Property(e => e.Quarter)
            .HasDefaultValue(null)
            .HasColumnName("quarter");

        entityTypeBuilder
            .Property(e => e.QuarterSecondsRemaining)
            .HasDefaultValue(null)
            .HasColumnName("quarter_seconds_remaining");
    }
}
