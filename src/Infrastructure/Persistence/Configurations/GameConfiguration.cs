using Football.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football.Infrastructure.Persistence.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("game", "football");

        entityTypeBuilder.HasKey(e => e.Id).HasName("game_pkey");

        entityTypeBuilder.HasIndex(e => e.Week, "ix_game_week");

        entityTypeBuilder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");

        entityTypeBuilder.Property(e => e.AwayTeam)
            .HasMaxLength(3)
            .HasColumnName("away_team");

        entityTypeBuilder.Property(e => e.HomeTeam)
            .HasMaxLength(3)
            .HasColumnName("home_team");

        entityTypeBuilder.Property(e => e.Quarter)
            .HasColumnName("quarter");

        entityTypeBuilder.Property(e => e.QuarterSecondsRemaining)
            .HasColumnName("quarter_seconds_remaining");

        entityTypeBuilder.Property(e => e.State)
            .HasConversion<string>()
            .HasDefaultValue(null)
            .HasMaxLength(10)
            .HasColumnName("state");

        entityTypeBuilder.Property(e => e.Week)
            .HasColumnName("week");
    }
}
