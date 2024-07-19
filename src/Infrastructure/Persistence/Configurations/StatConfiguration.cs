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
            .IsRequired()
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
            .IsRequired()
            .HasColumnName("score");

        entityTypeBuilder
            .Property(e => e.PassingYards)
            .IsRequired()
            .HasColumnName("passing_yards");

        entityTypeBuilder
            .Property(e => e.Sacks)
            .IsRequired()
            .HasColumnName("sacks");

        entityTypeBuilder
            .Property(e => e.Punts)
            .IsRequired()
            .HasColumnName("punts");

        entityTypeBuilder
            .Property(e => e.ReturnYards)
            .IsRequired()
            .HasColumnName("return_yards");
    }
}
