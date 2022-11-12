using Football.Core.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Football.Core.Persistence.Contexts
{
    public class FootballDbContext : DbContext, IFootballDbContext
    {
        public virtual DbSet<PlayEntity> Play => Set<PlayEntity>();

        public virtual DbSet<GameEntity> Game => Set<GameEntity>();

        public virtual DbSet<StatEntity> Stat => Set<StatEntity>();

        public virtual DbSet<TimeEntity> Time => Set<TimeEntity>();

        public FootballDbContext() { }

        public FootballDbContext(DbContextOptions<FootballDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameEntity>(entity =>
            {
                entity.ToTable("game");

                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AwayTeam)
                    .HasColumnName("away_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.HomeTeam)
                    .HasColumnName("home_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Week).HasColumnName("week");
            });

            modelBuilder.Entity<PlayEntity>(entity =>
            {
                entity.ToTable("play");

                entity.HasIndex(e => e.GameId, "game_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AirYards).HasColumnName("air_yards");

                entity.Property(e => e.AssistTackle).HasColumnName("assist_tackle");

                entity.Property(e => e.AwayTeam)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("away_team");

                entity.Property(e => e.AwayTimeoutsRemaining).HasColumnName("away_timeouts_remaining");

                entity.Property(e => e.CompletePass).HasColumnName("complete_pass");

                entity.Property(e => e.DefensiveExtraPointAttempt).HasColumnName("defensive_extra_point_attempt");

                entity.Property(e => e.DefensiveExtraPointConv).HasColumnName("defensive_extra_point_conv");

                entity.Property(e => e.DefensiveTwoPointAttempt).HasColumnName("defensive_two_point_attempt");

                entity.Property(e => e.DefensiveTwoPointConv).HasColumnName("defensive_two_point_conv");

                entity.Property(e => e.Defteam)
                    .IsRequired()
                    .HasMaxLength(4)
                    .HasColumnName("defteam");

                entity.Property(e => e.DefteamScore).HasColumnName("defteam_score");

                entity.Property(e => e.DefteamTimeoutsRemaining).HasColumnName("defteam_timeouts_remaining");

                entity.Property(e => e.Desc)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("desc");

                entity.Property(e => e.Down).HasColumnName("down");

                entity.Property(e => e.Drive).HasColumnName("drive");

                entity.Property(e => e.ExtraPointAttempt).HasColumnName("extra_point_attempt");

                entity.Property(e => e.ExtraPointResult)
                    .HasMaxLength(7)
                    .HasColumnName("extra_point_result");

                entity.Property(e => e.FieldGoalAttempt).HasColumnName("field_goal_attempt");

                entity.Property(e => e.FieldGoalResult)
                    .HasMaxLength(7)
                    .HasColumnName("field_goal_result");

                entity.Property(e => e.FirstDownPass).HasColumnName("first_down_pass");

                entity.Property(e => e.FirstDownPenalty).HasColumnName("first_down_penalty");

                entity.Property(e => e.FirstDownRush).HasColumnName("first_down_rush");

                entity.Property(e => e.ForcedFumblePlayer1PlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("forced_fumble_player_1_player_id");

                entity.Property(e => e.ForcedFumblePlayer1PlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("forced_fumble_player_1_player_name");

                entity.Property(e => e.ForcedFumblePlayer1Team)
                    .HasMaxLength(3)
                    .HasColumnName("forced_fumble_player_1_team");

                entity.Property(e => e.ForcedFumblePlayer2PlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("forced_fumble_player_2_player_id");

                entity.Property(e => e.ForcedFumblePlayer2PlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("forced_fumble_player_2_player_name");

                entity.Property(e => e.ForcedFumblePlayer2Team)
                    .HasMaxLength(3)
                    .HasColumnName("forced_fumble_player_2_team");

                entity.Property(e => e.FourthDownConverted).HasColumnName("fourth_down_converted");

                entity.Property(e => e.FourthDownFailed).HasColumnName("fourth_down_failed");

                entity.Property(e => e.Fumble).HasColumnName("fumble");

                entity.Property(e => e.FumbleForced).HasColumnName("fumble_forced");

                entity.Property(e => e.FumbleLost).HasColumnName("fumble_lost");

                entity.Property(e => e.FumbleNotForced).HasColumnName("fumble_not_forced");

                entity.Property(e => e.FumbleOutOfBounds).HasColumnName("fumble_out_of_bounds");

                entity.Property(e => e.FumbleRecovery1PlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("fumble_recovery_1_player_id");

                entity.Property(e => e.FumbleRecovery1PlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("fumble_recovery_1_player_name");

                entity.Property(e => e.FumbleRecovery1Team)
                    .HasMaxLength(3)
                    .HasColumnName("fumble_recovery_1_team");

                entity.Property(e => e.FumbleRecovery1Yards).HasColumnName("fumble_recovery_1_yards");

                entity.Property(e => e.FumbleRecovery2PlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("fumble_recovery_2_player_id");

                entity.Property(e => e.FumbleRecovery2PlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("fumble_recovery_2_player_name");

                entity.Property(e => e.FumbleRecovery2Team)
                    .HasMaxLength(3)
                    .HasColumnName("fumble_recovery_2_team");

                entity.Property(e => e.FumbleRecovery2Yards).HasColumnName("fumble_recovery_2_yards");

                entity.Property(e => e.Fumbled1PlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("fumbled_1_player_id");

                entity.Property(e => e.Fumbled1PlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("fumbled_1_player_name");

                entity.Property(e => e.Fumbled1Team)
                    .HasMaxLength(3)
                    .HasColumnName("fumbled_1_team");

                entity.Property(e => e.Fumbled2PlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("fumbled_2_player_id");

                entity.Property(e => e.Fumbled2PlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("fumbled_2_player_name");

                entity.Property(e => e.Fumbled2Team)
                    .HasMaxLength(3)
                    .HasColumnName("fumbled_2_team");

                entity.Property(e => e.GameDate)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("game_date");

                entity.Property(e => e.GameHalf)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasColumnName("game_half");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.GameSecondsRemaining).HasColumnName("game_seconds_remaining");

                entity.Property(e => e.GoalToGo).HasColumnName("goal_to_go");

                entity.Property(e => e.HalfSecondsRemaining).HasColumnName("half_seconds_remaining");

                entity.Property(e => e.HomeTeam)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("home_team");

                entity.Property(e => e.HomeTimeoutsRemaining).HasColumnName("home_timeouts_remaining");

                entity.Property(e => e.IncompletePass).HasColumnName("incomplete_pass");

                entity.Property(e => e.Interception).HasColumnName("interception");

                entity.Property(e => e.InterceptionPlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("interception_player_id");

                entity.Property(e => e.InterceptionPlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("interception_player_name");

                entity.Property(e => e.KickDistance).HasColumnName("kick_distance");

                entity.Property(e => e.KickerPlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("kicker_player_id");

                entity.Property(e => e.KickerPlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("kicker_player_name");

                entity.Property(e => e.KickoffAttempt).HasColumnName("kickoff_attempt");

                entity.Property(e => e.KickoffReturnerPlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("kickoff_returner_player_id");

                entity.Property(e => e.KickoffReturnerPlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("kickoff_returner_player_name");

                entity.Property(e => e.NoHuddle).HasColumnName("no_huddle");

                entity.Property(e => e.OwnKickoffRecovery).HasColumnName("own_kickoff_recovery");

                entity.Property(e => e.OwnKickoffRecoveryTd).HasColumnName("own_kickoff_recovery_td");

                entity.Property(e => e.PassAttempt).HasColumnName("pass_attempt");

                entity.Property(e => e.PassDefense1PlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("pass_defense_1_player_id");

                entity.Property(e => e.PassDefense1PlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("pass_defense_1_player_name");

                entity.Property(e => e.PassDefense2PlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("pass_defense_2_player_id");

                entity.Property(e => e.PassDefense2PlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("pass_defense_2_player_name");

                entity.Property(e => e.PassLength)
                    .HasMaxLength(5)
                    .HasColumnName("pass_length");

                entity.Property(e => e.PassLocation)
                    .HasMaxLength(6)
                    .HasColumnName("pass_location");

                entity.Property(e => e.PassTouchdown).HasColumnName("pass_touchdown");

                entity.Property(e => e.PasserPlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("passer_player_id");

                entity.Property(e => e.PasserPlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("passer_player_name");

                entity.Property(e => e.Penalty).HasColumnName("penalty");

                entity.Property(e => e.PenaltyPlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("penalty_player_id");

                entity.Property(e => e.PenaltyPlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("penalty_player_name");

                entity.Property(e => e.PenaltyTeam)
                    .HasMaxLength(3)
                    .HasColumnName("penalty_team");

                entity.Property(e => e.PenaltyType)
                    .HasMaxLength(80)
                    .HasColumnName("penalty_type");

                entity.Property(e => e.PenaltyYards).HasColumnName("penalty_yards");

                entity.Property(e => e.PlayId).HasColumnName("play_id");

                entity.Property(e => e.PlayType)
                    .HasMaxLength(20)
                    .HasColumnName("play_type");

                entity.Property(e => e.Posteam)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("posteam");

                entity.Property(e => e.PosteamScore).HasColumnName("posteam_score");

                entity.Property(e => e.PosteamTimeoutsRemaining).HasColumnName("posteam_timeouts_remaining");

                entity.Property(e => e.PosteamType)
                    .IsRequired()
                    .HasMaxLength(4)
                    .HasColumnName("posteam_type");

                entity.Property(e => e.PuntAttempt).HasColumnName("punt_attempt");

                entity.Property(e => e.PuntBlocked).HasColumnName("punt_blocked");

                entity.Property(e => e.PuntReturnerPlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("punt_returner_player_id");

                entity.Property(e => e.PuntReturnerPlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("punt_returner_player_name");

                entity.Property(e => e.PunterPlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("punter_player_id");

                entity.Property(e => e.PunterPlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("punter_player_name");

                entity.Property(e => e.QbDropback).HasColumnName("qb_dropback");

                entity.Property(e => e.QbHit).HasColumnName("qb_hit");

                entity.Property(e => e.QbKneel).HasColumnName("qb_kneel");

                entity.Property(e => e.QbScramble).HasColumnName("qb_scramble");

                entity.Property(e => e.QbSpike).HasColumnName("qb_spike");

                entity.Property(e => e.Qtr).HasColumnName("qtr");

                entity.Property(e => e.QuarterEnd).HasColumnName("quarter_end");

                entity.Property(e => e.QuarterSecondsRemaining).HasColumnName("quarter_seconds_remaining");

                entity.Property(e => e.ReceiverPlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("receiver_player_id");

                entity.Property(e => e.ReceiverPlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("receiver_player_name");

                entity.Property(e => e.ReplayOrChallenge).HasColumnName("replay_or_challenge");

                entity.Property(e => e.ReplayOrChallengeResult)
                    .HasMaxLength(8)
                    .HasColumnName("replay_or_challenge_result");

                entity.Property(e => e.ReturnTeam)
                    .HasMaxLength(3)
                    .HasColumnName("return_team");

                entity.Property(e => e.ReturnTouchdown).HasColumnName("return_touchdown");

                entity.Property(e => e.ReturnYards).HasColumnName("return_yards");

                entity.Property(e => e.RunGap)
                    .HasMaxLength(6)
                    .HasColumnName("run_gap");

                entity.Property(e => e.RunLocation)
                    .HasMaxLength(6)
                    .HasColumnName("run_location");

                entity.Property(e => e.RushAttempt).HasColumnName("rush_attempt");

                entity.Property(e => e.RushTouchdown).HasColumnName("rush_touchdown");

                entity.Property(e => e.RusherPlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("rusher_player_id");

                entity.Property(e => e.RusherPlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("rusher_player_name");

                entity.Property(e => e.Sack).HasColumnName("sack");

                entity.Property(e => e.Safety).HasColumnName("safety");

                entity.Property(e => e.Shotgun).HasColumnName("shotgun");

                entity.Property(e => e.SideOfField)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("side_of_field");

                entity.Property(e => e.SoloTackle).HasColumnName("solo_tackle");

                entity.Property(e => e.SoloTackle1PlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("solo_tackle_1_player_id");

                entity.Property(e => e.SoloTackle1PlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("solo_tackle_1_player_name");

                entity.Property(e => e.SoloTackle1Team)
                    .HasMaxLength(3)
                    .HasColumnName("solo_tackle_1_team");

                entity.Property(e => e.SoloTackle2PlayerId)
                    .HasMaxLength(10)
                    .HasColumnName("solo_tackle_2_player_id");

                entity.Property(e => e.SoloTackle2PlayerName)
                    .HasMaxLength(30)
                    .HasColumnName("solo_tackle_2_player_name");

                entity.Property(e => e.SoloTackle2Team)
                    .HasMaxLength(3)
                    .HasColumnName("solo_tackle_2_team");

                entity.Property(e => e.Sp).HasColumnName("sp");

                entity.Property(e => e.TackleForLoss).HasColumnName("tackle_for_loss");

                entity.Property(e => e.TdTeam)
                    .HasMaxLength(3)
                    .HasColumnName("td_team");

                entity.Property(e => e.ThirdDownConverted).HasColumnName("third_down_converted");

                entity.Property(e => e.ThirdDownFailed).HasColumnName("third_down_failed");

                entity.Property(e => e.Time)
                    .HasMaxLength(10)
                    .HasColumnName("time");

                entity.Property(e => e.Timeout).HasColumnName("timeout");

                entity.Property(e => e.TimeoutTeam)
                    .HasMaxLength(3)
                    .HasColumnName("timeout_team");

                entity.Property(e => e.TotalAwayScore).HasColumnName("total_away_score");

                entity.Property(e => e.TotalHomeScore).HasColumnName("total_home_score");

                entity.Property(e => e.Touchback).HasColumnName("touchback");

                entity.Property(e => e.Touchdown).HasColumnName("touchdown");

                entity.Property(e => e.TwoPointAttempt).HasColumnName("two_point_attempt");

                entity.Property(e => e.TwoPointConvResult)
                    .HasMaxLength(7)
                    .HasColumnName("two_point_conv_result");

                entity.Property(e => e.Week).HasColumnName("week");

                entity.Property(e => e.Yardline100).HasColumnName("yardline_100");

                entity.Property(e => e.YardsAfterCatch).HasColumnName("yards_after_catch");

                entity.Property(e => e.YardsGained).HasColumnName("yards_gained");

                entity.Property(e => e.Ydsnet).HasColumnName("ydsnet");

                entity.Property(e => e.Ydstogo).HasColumnName("ydstogo");

                entity.Property(e => e.Yrdln)
                    .HasMaxLength(6)
                    .HasColumnName("yrdln");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Plays)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("play_ibfk_1");
            });

            modelBuilder.Entity<StatEntity>(entity =>
            {
                entity.HasKey(e => new { e.GameId, e.Team })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("stat");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Team)
                    .HasMaxLength(3)
                    .HasColumnName("team");

                entity.Property(e => e.AirYards).HasColumnName("air_yards");

                entity.Property(e => e.Punts).HasColumnName("punts");

                entity.Property(e => e.ReturnYards).HasColumnName("return_yards");

                entity.Property(e => e.Sacks).HasColumnName("sacks");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("stat_ibfk_1");
            });

            modelBuilder.Entity<TimeEntity>(entity =>
            {
                entity.HasKey(e => e.GameId)
                    .HasName("PRIMARY");

                entity.ToTable("time");

                entity.Property(e => e.GameId)
                    .ValueGeneratedNever()
                    .HasColumnName("game_id");

                entity.Property(e => e.Quarter).HasColumnName("quarter");

                entity.Property(e => e.QuarterSecondsRemaining).HasColumnName("quarter_seconds_remaining");

                entity.HasOne(d => d.Game)
                    .WithOne(p => p.Time)
                    .HasForeignKey<TimeEntity>(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("time_ibfk_1");
            });
        }
    }
}
