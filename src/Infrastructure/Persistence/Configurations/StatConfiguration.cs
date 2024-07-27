using Football.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football.Infrastructure.Persistence.Configurations;

public class StatConfiguration : IEntityTypeConfiguration<Stat>
{
    public void Configure(EntityTypeBuilder<Stat> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("stat");

        entityTypeBuilder
            .HasKey(e => new { e.GameId, e.Team })
            .HasName("PRIMARY");

        entityTypeBuilder
            .Property(e => e.GameId)
            .HasColumnName("game_id");

        entityTypeBuilder
            .HasOne(d => d.Game)
            .WithMany(p => p.Stats)
            .HasForeignKey(d => d.GameId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Stat_GameId");

        entityTypeBuilder
            .Property(e => e.Team)
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnName("team");

        entityTypeBuilder
            .Property(e => e.Score)
            .HasColumnName("score");

        entityTypeBuilder
            .Property(e => e.PassingYards)
            .HasColumnName("passing_yards");

        entityTypeBuilder
            .Property(e => e.Sacks)
            .HasColumnName("sacks");

        entityTypeBuilder
            .Property(e => e.Punts)
            .HasColumnName("punts");

        entityTypeBuilder
            .Property(e => e.ReturnYards)
            .HasColumnName("return_yards");
    }
}
