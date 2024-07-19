using Football.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football.Infrastructure.Persistence.Configurations;

public class PlayConfiguration : IEntityTypeConfiguration<Play>
{
    public void Configure(EntityTypeBuilder<Play> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("play");

        entityTypeBuilder
            .HasKey(x => x.Id)
            .HasName("PRIMARY");

        entityTypeBuilder
            .Property(e => e.Id)
            .HasColumnName("id");

        entityTypeBuilder
            .Property(e => e.PlayId)
            .HasColumnName("play_id");

        entityTypeBuilder
            .Property(e => e.GameId)
            .HasColumnName("game_id");

        entityTypeBuilder
            .HasIndex(e => e.GameId, "game_id")
            .HasDatabaseName("IX_Play_GameId");

        entityTypeBuilder
            .HasOne(d => d.Game)
            .WithMany(p => p.Plays)
            .HasForeignKey(d => d.GameId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Play_GameId");

        entityTypeBuilder
            .Property(e => e.Week)
            .HasColumnName("week");

        entityTypeBuilder
            .Property(e => e.HomeTeam)
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnName("home_team");

        entityTypeBuilder
            .Property(e => e.HomeScore)
            .IsRequired()
            .HasColumnName("home_score");

        entityTypeBuilder
            .Property(e => e.AwayTeam)
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnName("away_team");

        entityTypeBuilder
            .Property(e => e.AwayScore)
            .IsRequired()
            .HasColumnName("away_score");

        entityTypeBuilder
            .Property(e => e.Qtr)
            .HasColumnName("qtr");

        entityTypeBuilder
            .Property(e => e.QuarterSecondsRemaining)
            .HasColumnName("quarter_seconds_remaining");

        entityTypeBuilder
            .HasIndex(p => new { p.Week, p.Qtr, p.QuarterSecondsRemaining })
            .IsDescending(false, false, true)
            .HasDatabaseName("IX_Play_Week-Quarter-QuarterSecondsRemaining");

        entityTypeBuilder
            .Property(e => e.HalfSecondsRemaining)
            .HasColumnName("half_seconds_remaining");

        entityTypeBuilder
            .Property(e => e.GameSecondsRemaining)
            .HasColumnName("game_seconds_remaining");

        entityTypeBuilder.Property(e => e.GameDate)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("game_date");

        entityTypeBuilder.Property(e => e.GameHalf)
            .IsRequired()
            .HasMaxLength(8)
            .HasColumnName("game_half");

        entityTypeBuilder
            .Property(e => e.Desc)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName("desc");

        entityTypeBuilder
            .Property(e => e.Time)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("time");

        entityTypeBuilder
            .Property(e => e.SideOfField)
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnName("side_of_field");

        entityTypeBuilder
            .Property(e => e.Yrdln)
            .IsRequired()
            .HasMaxLength(6)
            .HasColumnName("yrdln");

        entityTypeBuilder
            .Property(e => e.PlayType)
            .HasMaxLength(20)
            .HasColumnName("play_type");

        entityTypeBuilder
            .Property(e => e.Down)
            .HasColumnName("down");

        entityTypeBuilder
            .Property(e => e.GoalToGo)
            .HasColumnName("goal_to_go");

        entityTypeBuilder
            .Property(e => e.Yardline100)
            .HasColumnName("yardline_100");

        entityTypeBuilder
            .Property(e => e.Drive)
            .HasColumnName("drive");

        entityTypeBuilder
            .Property(e => e.Ydstogo)
            .HasColumnName("ydstogo");

        entityTypeBuilder
            .Property(e => e.Sp)
            .IsRequired()
            .HasColumnName("sp");

        entityTypeBuilder
            .Property(e => e.QuarterEnd)
            .IsRequired()
            .HasColumnName("quarter_end");

        entityTypeBuilder
            .Property(e => e.Touchdown)
            .IsRequired()
            .HasColumnName("touchdown");

        entityTypeBuilder
            .Property(e => e.Shotgun)
            .IsRequired()
            .HasColumnName("shotgun");

        entityTypeBuilder
            .Property(e => e.NoHuddle)
            .IsRequired()
            .HasColumnName("no_huddle");

        entityTypeBuilder
            .Property(e => e.QbDropback)
            .IsRequired()
            .HasColumnName("qb_dropback");

        entityTypeBuilder
            .Property(e => e.QbKneel)
            .IsRequired()
            .HasColumnName("qb_kneel");

        entityTypeBuilder
            .Property(e => e.QbSpike)
            .IsRequired()
            .HasColumnName("qb_spike");

        entityTypeBuilder
            .Property(e => e.QbScramble)
            .IsRequired()
            .HasColumnName("qb_scramble");

        entityTypeBuilder
            .Property(e => e.QbHit)
            .IsRequired()
            .HasColumnName("qb_hit");

        entityTypeBuilder
            .Property(e => e.FirstDownPass)
            .IsRequired()
            .HasColumnName("first_down_pass");

        entityTypeBuilder
            .Property(e => e.FirstDownRush)
            .IsRequired()
            .HasColumnName("first_down_rush");

        entityTypeBuilder
            .Property(e => e.FirstDownPenalty)
            .IsRequired()
            .HasColumnName("first_down_penalty");

        entityTypeBuilder
            .Property(e => e.ThirdDownConverted)
            .IsRequired()
            .HasColumnName("third_down_converted");

        entityTypeBuilder
            .Property(e => e.ThirdDownFailed)
            .IsRequired()
            .HasColumnName("third_down_failed");

        entityTypeBuilder
            .Property(e => e.FourthDownConverted)
            .IsRequired()
            .HasColumnName("fourth_down_converted");

        entityTypeBuilder
            .Property(e => e.FourthDownFailed)
            .IsRequired()
            .HasColumnName("fourth_down_failed");

        entityTypeBuilder
            .Property(e => e.PassAttempt)
            .IsRequired()
            .HasColumnName("pass_attempt");

        entityTypeBuilder
            .Property(e => e.PassTouchdown)
            .IsRequired()
            .HasColumnName("pass_touchdown");

        entityTypeBuilder
            .Property(e => e.CompletePass)
            .IsRequired()
            .HasColumnName("complete_pass");

        entityTypeBuilder
            .Property(e => e.IncompletePass)
            .IsRequired()
            .HasColumnName("incomplete_pass");

        entityTypeBuilder
            .Property(e => e.Interception)
            .IsRequired()
            .HasColumnName("interception");

        entityTypeBuilder
            .Property(e => e.RushAttempt)
            .IsRequired()
            .HasColumnName("rush_attempt");

        entityTypeBuilder
            .Property(e => e.RushTouchdown)
            .IsRequired()
            .HasColumnName("rush_touchdown");

        entityTypeBuilder
            .Property(e => e.Fumble)
            .IsRequired()
            .HasColumnName("fumble");

        entityTypeBuilder
            .Property(e => e.FumbleForced)
            .IsRequired()
            .HasColumnName("fumble_forced");

        entityTypeBuilder
            .Property(e => e.FumbleNotForced)
            .IsRequired()
            .HasColumnName("fumble_not_forced");

        entityTypeBuilder
            .Property(e => e.FumbleLost)
            .IsRequired()
            .HasColumnName("fumble_lost");

        entityTypeBuilder
            .Property(e => e.FumbleOutOfBounds)
            .IsRequired()
            .HasColumnName("fumble_out_of_bounds");

        entityTypeBuilder
            .Property(e => e.Touchback)
            .IsRequired()
            .HasColumnName("touchback");

        entityTypeBuilder
            .Property(e => e.ReturnTouchdown)
            .IsRequired()
            .HasColumnName("return_touchdown");

        entityTypeBuilder
            .Property(e => e.PuntAttempt)
            .IsRequired()
            .HasColumnName("punt_attempt");

        entityTypeBuilder
            .Property(e => e.PuntBlocked)
            .IsRequired()
            .HasColumnName("punt_blocked");

        entityTypeBuilder
            .Property(e => e.KickoffAttempt)
            .IsRequired()
            .HasColumnName("kickoff_attempt");

        entityTypeBuilder
            .Property(e => e.OwnKickoffRecovery)
            .IsRequired()
            .HasColumnName("own_kickoff_recovery");

        entityTypeBuilder
            .Property(e => e.OwnKickoffRecoveryTd)
            .IsRequired()
            .HasColumnName("own_kickoff_recovery_td");

        entityTypeBuilder
            .Property(e => e.ExtraPointAttempt)
            .IsRequired()
            .HasColumnName("extra_point_attempt");

        entityTypeBuilder
            .Property(e => e.TwoPointAttempt)
            .IsRequired()
            .HasColumnName("two_point_attempt");

        entityTypeBuilder
            .Property(e => e.FieldGoalAttempt)
            .IsRequired()
            .HasColumnName("field_goal_attempt");

        entityTypeBuilder
            .Property(e => e.SoloTackle)
            .IsRequired()
            .HasColumnName("solo_tackle");

        entityTypeBuilder
            .Property(e => e.AssistTackle)
            .IsRequired()
            .HasColumnName("assist_tackle");

        entityTypeBuilder
            .Property(e => e.TackleForLoss)
            .IsRequired()
            .HasColumnName("tackle_for_loss");

        entityTypeBuilder
            .Property(e => e.Sack)
            .IsRequired()
            .HasColumnName("sack");

        entityTypeBuilder
            .Property(e => e.Safety)
            .IsRequired()
            .HasColumnName("safety");

        entityTypeBuilder
            .Property(e => e.Penalty)
            .IsRequired()
            .HasColumnName("penalty");

        entityTypeBuilder
            .Property(e => e.ReplayOrChallenge)
            .IsRequired()
            .HasColumnName("replay_or_challenge");

        entityTypeBuilder
            .Property(e => e.DefensiveTwoPointAttempt)
            .IsRequired()
            .HasColumnName("defensive_two_point_attempt");

        entityTypeBuilder
            .Property(e => e.DefensiveTwoPointConv)
            .IsRequired()
            .HasColumnName("defensive_two_point_conv");

        entityTypeBuilder
            .Property(e => e.DefensiveExtraPointAttempt)
            .IsRequired()
            .HasColumnName("defensive_extra_point_attempt");

        entityTypeBuilder
            .Property(e => e.DefensiveExtraPointConv)
            .IsRequired()
            .HasColumnName("defensive_extra_point_conv");

        entityTypeBuilder
            .Property(e => e.Posteam)
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnName("posteam");

        entityTypeBuilder
            .Property(e => e.PosteamType)
            .IsRequired()
            .HasMaxLength(4)
            .HasColumnName("posteam_type");

        entityTypeBuilder
            .Property(e => e.PosteamScore)
            .HasColumnName("posteam_score");

        entityTypeBuilder
            .Property(e => e.Defteam)
            .IsRequired()
            .HasMaxLength(4)
            .HasColumnName("defteam");

        entityTypeBuilder
            .Property(e => e.DefteamScore)
            .HasColumnName("defteam_score");

        entityTypeBuilder
            .Property(e => e.TdTeam)
            .HasMaxLength(3)
            .HasColumnName("td_team");

        entityTypeBuilder
            .Property(e => e.Ydsnet)
            .HasColumnName("ydsnet");

        entityTypeBuilder
            .Property(e => e.YardsGained)
            .HasColumnName("yards_gained");

        entityTypeBuilder
            .Property(e => e.PassLength)
            .HasMaxLength(5)
            .HasColumnName("pass_length");

        entityTypeBuilder
            .Property(e => e.PassLocation)
            .HasMaxLength(6)
            .HasColumnName("pass_location");

        entityTypeBuilder
            .Property(e => e.AirYards)
            .HasColumnName("air_yards");

        entityTypeBuilder
            .Property(e => e.YardsAfterCatch)
            .HasColumnName("yards_after_catch");

        entityTypeBuilder
            .Property(e => e.RunLocation)
            .HasMaxLength(6)
            .HasColumnName("run_location");

        entityTypeBuilder
            .Property(e => e.RunGap)
            .HasMaxLength(6)
            .HasColumnName("run_gap");

        entityTypeBuilder
            .Property(e => e.FieldGoalResult)
            .HasMaxLength(7)
            .HasColumnName("field_goal_result");

        entityTypeBuilder
            .Property(e => e.ExtraPointResult)
            .HasMaxLength(7)
            .HasColumnName("extra_point_result");

        entityTypeBuilder
            .Property(e => e.TwoPointConvResult)
            .HasMaxLength(7)
            .HasColumnName("two_point_conv_result");

        entityTypeBuilder
            .Property(e => e.ReturnTeam)
            .HasMaxLength(3)
            .HasColumnName("return_team");

        entityTypeBuilder
            .Property(e => e.ReturnYards)
            .HasColumnName("return_yards");

        entityTypeBuilder
            .Property(e => e.KickDistance)
            .HasColumnName("kick_distance");

        entityTypeBuilder
            .Property(e => e.PenaltyTeam)
            .HasMaxLength(3)
            .HasColumnName("penalty_team");

        entityTypeBuilder
            .Property(e => e.PenaltyType)
            .HasMaxLength(80)
            .HasColumnName("penalty_type");

        entityTypeBuilder
            .Property(e => e.PenaltyYards)
            .HasColumnName("penalty_yards");

        entityTypeBuilder
            .Property(e => e.ReplayOrChallengeResult)
            .HasMaxLength(8)
            .HasColumnName("replay_or_challenge_result");

        entityTypeBuilder
            .Property(e => e.Timeout)
            .IsRequired()
            .HasColumnName("timeout");

        entityTypeBuilder
            .Property(e => e.TimeoutTeam)
            .HasMaxLength(3)
            .HasColumnName("timeout_team");

        entityTypeBuilder
            .Property(e => e.HomeTimeoutsRemaining)
            .HasColumnName("home_timeouts_remaining");

        entityTypeBuilder
            .Property(e => e.AwayTimeoutsRemaining)
            .HasColumnName("away_timeouts_remaining");

        entityTypeBuilder
            .Property(e => e.PosteamTimeoutsRemaining)
            .HasColumnName("posteam_timeouts_remaining");

        entityTypeBuilder
            .Property(e => e.DefteamTimeoutsRemaining)
            .HasColumnName("defteam_timeouts_remaining");
    }
}
