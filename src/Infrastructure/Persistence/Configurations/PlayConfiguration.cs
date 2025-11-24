using Football.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football.Infrastructure.Persistence.Configurations;

public class PlayConfiguration : IEntityTypeConfiguration<Play>
{
    public void Configure(EntityTypeBuilder<Play> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(e => e.Id).HasName("play_pkey");

        entityTypeBuilder.ToTable("play", "football");

        entityTypeBuilder.HasIndex(e => e.GameId, "ix_play_game_id");

        entityTypeBuilder.HasIndex(e => new { e.Week, e.Quarter, e.QuarterSecondsRemaining }, "ix_play_week_quarter_seconds");

        entityTypeBuilder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");

        entityTypeBuilder.Property(e => e.AirYards)
            .HasColumnName("air_yards");

        entityTypeBuilder.Property(e => e.AssistTackle)
            .HasColumnName("assist_tackle");

        entityTypeBuilder.Property(e => e.AwayScore)
            .HasColumnName("away_score");

        entityTypeBuilder.Property(e => e.AwayTeam)
            .HasMaxLength(3)
            .HasColumnName("away_team");

        entityTypeBuilder.Property(e => e.AwayTimeoutsRemaining)
            .HasColumnName("away_timeouts_remaining");

        entityTypeBuilder.Property(e => e.CompletePass)
            .HasColumnName("complete_pass");

        entityTypeBuilder.Property(e => e.DefensiveExtraPointAttempt)
            .HasColumnName("defensive_extra_point_attempt");

        entityTypeBuilder.Property(e => e.DefensiveExtraPointConv)
            .HasColumnName("defensive_extra_point_conv");

        entityTypeBuilder.Property(e => e.DefensiveTwoPointAttempt)
            .HasColumnName("defensive_two_point_attempt");

        entityTypeBuilder.Property(e => e.DefensiveTwoPointConv)
            .HasColumnName("defensive_two_point_conv");

        entityTypeBuilder.Property(e => e.Defteam)
            .HasMaxLength(4)
            .HasColumnName("defteam");

        entityTypeBuilder.Property(e => e.DefteamScore)
            .HasColumnName("defteam_score");

        entityTypeBuilder.Property(e => e.DefteamTimeoutsRemaining)
            .HasColumnName("defteam_timeouts_remaining");

        entityTypeBuilder.Property(e => e.Description)
            .HasMaxLength(1000)
            .HasColumnName("description");

        entityTypeBuilder.Property(e => e.Down)
            .HasColumnName("down");

        entityTypeBuilder.Property(e => e.Drive)
            .HasColumnName("drive");

        entityTypeBuilder.Property(e => e.ExtraPointAttempt)
            .HasColumnName("extra_point_attempt");

        entityTypeBuilder.Property(e => e.ExtraPointResult)
            .HasMaxLength(7)
            .HasColumnName("extra_point_result");

        entityTypeBuilder.Property(e => e.FieldGoalAttempt)
            .HasColumnName("field_goal_attempt");

        entityTypeBuilder.Property(e => e.FieldGoalResult)
            .HasMaxLength(7)
            .HasColumnName("field_goal_result");

        entityTypeBuilder.Property(e => e.FirstDownPass)
            .HasColumnName("first_down_pass");

        entityTypeBuilder.Property(e => e.FirstDownPenalty)
            .HasColumnName("first_down_penalty");

        entityTypeBuilder.Property(e => e.FirstDownRush)
            .HasColumnName("first_down_rush");

        entityTypeBuilder.Property(e => e.FourthDownConverted)
            .HasColumnName("fourth_down_converted");

        entityTypeBuilder.Property(e => e.FourthDownFailed)
            .HasColumnName("fourth_down_failed");

        entityTypeBuilder.Property(e => e.Fumble)
            .HasColumnName("fumble");

        entityTypeBuilder.Property(e => e.FumbleForced)
            .HasColumnName("fumble_forced");

        entityTypeBuilder.Property(e => e.FumbleLost)
            .HasColumnName("fumble_lost");

        entityTypeBuilder.Property(e => e.FumbleNotForced)
            .HasColumnName("fumble_not_forced");

        entityTypeBuilder.Property(e => e.FumbleOutOfBounds)
            .HasColumnName("fumble_out_of_bounds");

        entityTypeBuilder.Property(e => e.GameDate)
            .HasMaxLength(10)
            .HasColumnName("game_date");

        entityTypeBuilder.Property(e => e.GameHalf)
            .HasMaxLength(8)
            .HasColumnName("game_half");

        entityTypeBuilder.Property(e => e.GameId)
            .HasColumnName("game_id");

        entityTypeBuilder.Property(e => e.GameSecondsRemaining)
            .HasColumnName("game_seconds_remaining");

        entityTypeBuilder.Property(e => e.GoalToGo)
            .HasColumnName("goal_to_go");

        entityTypeBuilder.Property(e => e.HalfSecondsRemaining)
            .HasColumnName("half_seconds_remaining");

        entityTypeBuilder.Property(e => e.HomeScore)
            .HasColumnName("home_score");

        entityTypeBuilder.Property(e => e.HomeTeam)
            .HasMaxLength(3)
            .HasColumnName("home_team");

        entityTypeBuilder.Property(e => e.HomeTimeoutsRemaining)
            .HasColumnName("home_timeouts_remaining");

        entityTypeBuilder.Property(e => e.IncompletePass)
            .HasColumnName("incomplete_pass");

        entityTypeBuilder.Property(e => e.Interception)
            .HasColumnName("interception");

        entityTypeBuilder.Property(e => e.KickDistance)
            .HasColumnName("kick_distance");

        entityTypeBuilder.Property(e => e.KickoffAttempt)
            .HasColumnName("kickoff_attempt");

        entityTypeBuilder.Property(e => e.NetYards)
            .HasColumnName("net_yards");

        entityTypeBuilder.Property(e => e.NoHuddle)
            .HasColumnName("no_huddle");

        entityTypeBuilder.Property(e => e.OwnKickoffRecovery)
            .HasColumnName("own_kickoff_recovery");

        entityTypeBuilder.Property(e => e.OwnKickoffRecoveryTd)
            .HasColumnName("own_kickoff_recovery_td");

        entityTypeBuilder.Property(e => e.PassAttempt)
            .HasColumnName("pass_attempt");

        entityTypeBuilder.Property(e => e.PassLength)
            .HasMaxLength(5)
            .HasColumnName("pass_length");

        entityTypeBuilder.Property(e => e.PassLocation)
            .HasMaxLength(6)
            .HasColumnName("pass_location");

        entityTypeBuilder.Property(e => e.PassTouchdown)
            .HasColumnName("pass_touchdown");

        entityTypeBuilder.Property(e => e.Penalty)
            .HasColumnName("penalty");

        entityTypeBuilder.Property(e => e.PenaltyTeam)
            .HasMaxLength(3)
            .HasColumnName("penalty_team");

        entityTypeBuilder.Property(e => e.PenaltyType)
            .HasMaxLength(80)
            .HasColumnName("penalty_type");

        entityTypeBuilder.Property(e => e.PenaltyYards)
            .HasColumnName("penalty_yards");

        entityTypeBuilder.Property(e => e.PlayId)
            .HasColumnName("play_id");

        entityTypeBuilder.Property(e => e.PlayTime)
            .HasMaxLength(10)
            .HasColumnName("play_time");

        entityTypeBuilder.Property(e => e.PlayType)
            .HasMaxLength(20)
            .HasColumnName("play_type");

        entityTypeBuilder.Property(e => e.Posteam)
            .HasMaxLength(3)
            .HasColumnName("posteam");

        entityTypeBuilder.Property(e => e.PosteamScore)
            .HasColumnName("posteam_score");

        entityTypeBuilder.Property(e => e.PosteamTimeoutsRemaining)
            .HasColumnName("posteam_timeouts_remaining");

        entityTypeBuilder.Property(e => e.PosteamType)
            .HasMaxLength(4)
            .HasColumnName("posteam_type");

        entityTypeBuilder.Property(e => e.PuntAttempt)
            .HasColumnName("punt_attempt");

        entityTypeBuilder.Property(e => e.PuntBlocked)
            .HasColumnName("punt_blocked");

        entityTypeBuilder.Property(e => e.QbDropback)
            .HasColumnName("qb_dropback");

        entityTypeBuilder.Property(e => e.QbHit)
            .HasColumnName("qb_hit");

        entityTypeBuilder.Property(e => e.QbKneel)
            .HasColumnName("qb_kneel");

        entityTypeBuilder.Property(e => e.QbScramble)
            .HasColumnName("qb_scramble");

        entityTypeBuilder.Property(e => e.QbSpike)
            .HasColumnName("qb_spike");

        entityTypeBuilder.Property(e => e.Quarter)
            .HasColumnName("quarter");

        entityTypeBuilder.Property(e => e.QuarterEnd)
            .HasColumnName("quarter_end");

        entityTypeBuilder.Property(e => e.QuarterSecondsRemaining)
            .HasColumnName("quarter_seconds_remaining");

        entityTypeBuilder.Property(e => e.ReplayOrChallenge)
            .HasColumnName("replay_or_challenge");

        entityTypeBuilder.Property(e => e.ReplayOrChallengeResult)
            .HasMaxLength(8)
            .HasColumnName("replay_or_challenge_result");

        entityTypeBuilder.Property(e => e.ReturnTeam)
            .HasMaxLength(3)
            .HasColumnName("return_team");

        entityTypeBuilder.Property(e => e.ReturnTouchdown)
            .HasColumnName("return_touchdown");

        entityTypeBuilder.Property(e => e.ReturnYards)
            .HasColumnName("return_yards");

        entityTypeBuilder.Property(e => e.RunGap)
            .HasMaxLength(6)
            .HasColumnName("run_gap");

        entityTypeBuilder.Property(e => e.RunLocation)
            .HasMaxLength(6)
            .HasColumnName("run_location");

        entityTypeBuilder.Property(e => e.RushAttempt)
            .HasColumnName("rush_attempt");

        entityTypeBuilder.Property(e => e.RushTouchdown)
            .HasColumnName("rush_touchdown");

        entityTypeBuilder.Property(e => e.Sack)
            .HasColumnName("sack");

        entityTypeBuilder.Property(e => e.Safety)
            .HasColumnName("safety");

        entityTypeBuilder.Property(e => e.Shotgun)
            .HasColumnName("shotgun");

        entityTypeBuilder.Property(e => e.SideOfField)
            .HasMaxLength(3)
            .HasColumnName("side_of_field");

        entityTypeBuilder.Property(e => e.SoloTackle)
            .HasColumnName("solo_tackle");

        entityTypeBuilder.Property(e => e.TackleForLoss)
            .HasColumnName("tackle_for_loss");

        entityTypeBuilder.Property(e => e.TdTeam)
            .HasMaxLength(3)
            .HasColumnName("td_team");

        entityTypeBuilder.Property(e => e.ThirdDownConverted)
            .HasColumnName("third_down_converted");

        entityTypeBuilder.Property(e => e.ThirdDownFailed)
            .HasColumnName("third_down_failed");

        entityTypeBuilder.Property(e => e.Timeout)
            .HasColumnName("timeout");

        entityTypeBuilder.Property(e => e.TimeoutTeam)
            .HasMaxLength(3)
            .HasColumnName("timeout_team");

        entityTypeBuilder.Property(e => e.Touchback)
            .HasColumnName("touchback");

        entityTypeBuilder.Property(e => e.Touchdown)
            .HasColumnName("touchdown");

        entityTypeBuilder.Property(e => e.TwoPointAttempt)
            .HasColumnName("two_point_attempt");

        entityTypeBuilder.Property(e => e.TwoPointConvResult)
            .HasMaxLength(7)
            .HasColumnName("two_point_conv_result");

        entityTypeBuilder.Property(e => e.Week)
            .HasColumnName("week");

        entityTypeBuilder.Property(e => e.YardLine)
            .HasMaxLength(6)
            .HasColumnName("yard_line");

        entityTypeBuilder.Property(e => e.Yardline100)
            .HasColumnName("yardline_100");

        entityTypeBuilder.Property(e => e.YardsAfterCatch)
            .HasColumnName("yards_after_catch");

        entityTypeBuilder.Property(e => e.YardsGained)
            .HasColumnName("yards_gained");

        entityTypeBuilder.Property(e => e.YardsToGo)
            .HasColumnName("yards_to_go");

        entityTypeBuilder.HasOne(d => d.Game).WithMany(p => p.Plays)
            .HasForeignKey(d => d.GameId)
            .HasConstraintName("play_game_id_fkey");
    }
}
