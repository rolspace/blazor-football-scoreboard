using Football.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football.Infrastructure.Persistence.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("game");

        entityTypeBuilder.HasKey(e => e.Id).HasName("PRIMARY");

        entityTypeBuilder.Property(e => e.Id).HasColumnName("id");

        entityTypeBuilder.Property(e => e.AwayTeam)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(3)
            .HasColumnName("away_team");

        entityTypeBuilder.Property(e => e.HomeTeam)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(3)
            .HasColumnName("home_team");

        entityTypeBuilder.Property(e => e.Week)
            .IsRequired()
            .HasColumnName("week");

        entityTypeBuilder.Property(e => e.State).HasColumnName("state_type").HasConversion<string>();

        entityTypeBuilder.Property(e => e.Quarter).HasColumnName("quarter");

        entityTypeBuilder.Property(e => e.QuarterSecondsRemaining).HasColumnName("quarter_seconds_remaining");
    }
}
