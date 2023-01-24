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
            .HasColumnName("away_team")
            .HasMaxLength(3)
            .IsUnicode(false);

        entityTypeBuilder.Property(e => e.HomeTeam)
            .HasColumnName("home_team")
            .HasMaxLength(3)
            .IsUnicode(false);

        entityTypeBuilder.Property(e => e.Week).HasColumnName("week");

        entityTypeBuilder.OwnsOne(e => e.Time)
            .Property(t => t.Quarter).HasColumnName("Quarter");

        entityTypeBuilder.OwnsOne(e => e.Time)
            .Property(t => t.QuarterSecondsRemaining).HasColumnName("QuarterSecondsRemaining");
    }
}
