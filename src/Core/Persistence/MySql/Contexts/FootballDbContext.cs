using Football.Core.Persistence.MySql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Football.Core.Persistence.MySql.Contexts
{
    public partial class FootballDbContext : DbContext
    {
        public FootballDbContext() { }

        public FootballDbContext(DbContextOptions<FootballDbContext> options)
            : base(options) { }

        public virtual DbSet<PlayEntity> Play { get; set; }
        
        public virtual DbSet<GameEntity> Game { get; set; }

        public virtual DbSet<StatEntity> Stat { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayEntity>(entity =>
            {
                entity.ToTable("play", "footballdb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AirYards).HasColumnName("air_yards");

                entity.Property(e => e.AssistTackle)
                    .HasColumnName("assist_tackle")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.AwayTeam)
                    .IsRequired()
                    .HasColumnName("away_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.AwayTimeoutsRemaining).HasColumnName("away_timeouts_remaining");

                entity.Property(e => e.CompletePass)
                    .HasColumnName("complete_pass")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.DefensiveExtraPointAttempt)
                    .HasColumnName("defensive_extra_point_attempt")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.DefensiveExtraPointConv)
                    .HasColumnName("defensive_extra_point_conv")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.DefensiveTwoPointAttempt)
                    .HasColumnName("defensive_two_point_attempt")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.DefensiveTwoPointConv)
                    .HasColumnName("defensive_two_point_conv")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Defteam)
                    .IsRequired()
                    .HasColumnName("defteam")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.DefteamScore).HasColumnName("defteam_score");

                entity.Property(e => e.DefteamTimeoutsRemaining).HasColumnName("defteam_timeouts_remaining");

                entity.Property(e => e.Desc)
                    .IsRequired()
                    .HasColumnName("desc")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Down).HasColumnName("down");

                entity.Property(e => e.Drive).HasColumnName("drive");

                entity.Property(e => e.ExtraPointAttempt)
                    .HasColumnName("extra_point_attempt")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.ExtraPointResult)
                    .HasColumnName("extra_point_result")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FieldGoalAttempt)
                    .HasColumnName("field_goal_attempt")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.FieldGoalResult)
                    .HasColumnName("field_goal_result")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FirstDownPass)
                    .HasColumnName("first_down_pass")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.FirstDownPenalty)
                    .HasColumnName("first_down_penalty")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.FirstDownRush)
                    .HasColumnName("first_down_rush")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.ForcedFumblePlayer1PlayerId)
                    .HasColumnName("forced_fumble_player_1_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ForcedFumblePlayer1PlayerName)
                    .HasColumnName("forced_fumble_player_1_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ForcedFumblePlayer1Team)
                    .HasColumnName("forced_fumble_player_1_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ForcedFumblePlayer2PlayerId)
                    .HasColumnName("forced_fumble_player_2_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ForcedFumblePlayer2PlayerName)
                    .HasColumnName("forced_fumble_player_2_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ForcedFumblePlayer2Team)
                    .HasColumnName("forced_fumble_player_2_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FourthDownConverted)
                    .HasColumnName("fourth_down_converted")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.FourthDownFailed)
                    .HasColumnName("fourth_down_failed")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Fumble)
                    .HasColumnName("fumble")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.FumbleForced)
                    .HasColumnName("fumble_forced")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.FumbleLost)
                    .HasColumnName("fumble_lost")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.FumbleNotForced)
                    .HasColumnName("fumble_not_forced")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.FumbleOutOfBounds)
                    .HasColumnName("fumble_out_of_bounds")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.FumbleRecovery1PlayerId)
                    .HasColumnName("fumble_recovery_1_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FumbleRecovery1PlayerName)
                    .HasColumnName("fumble_recovery_1_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FumbleRecovery1Team)
                    .HasColumnName("fumble_recovery_1_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FumbleRecovery1Yards).HasColumnName("fumble_recovery_1_yards");

                entity.Property(e => e.FumbleRecovery2PlayerId)
                    .HasColumnName("fumble_recovery_2_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FumbleRecovery2PlayerName)
                    .HasColumnName("fumble_recovery_2_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FumbleRecovery2Team)
                    .HasColumnName("fumble_recovery_2_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FumbleRecovery2Yards).HasColumnName("fumble_recovery_2_yards");

                entity.Property(e => e.Fumbled1PlayerId)
                    .HasColumnName("fumbled_1_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fumbled1PlayerName)
                    .HasColumnName("fumbled_1_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Fumbled1Team)
                    .HasColumnName("fumbled_1_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Fumbled2PlayerId)
                    .HasColumnName("fumbled_2_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fumbled2PlayerName)
                    .HasColumnName("fumbled_2_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Fumbled2Team)
                    .HasColumnName("fumbled_2_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.GameDate)
                    .IsRequired()
                    .HasColumnName("game_date")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GameHalf)
                    .IsRequired()
                    .HasColumnName("game_half")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.GameSecondsRemaining).HasColumnName("game_seconds_remaining");

                entity.Property(e => e.GoalToGo).HasColumnName("goal_to_go");

                entity.Property(e => e.HalfSecondsRemaining).HasColumnName("half_seconds_remaining");

                entity.Property(e => e.HomeTeam)
                    .IsRequired()
                    .HasColumnName("home_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.HomeTimeoutsRemaining).HasColumnName("home_timeouts_remaining");

                entity.Property(e => e.IncompletePass)
                    .HasColumnName("incomplete_pass")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Interception)
                    .HasColumnName("interception")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.InterceptionPlayerId)
                    .HasColumnName("interception_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.InterceptionPlayerName)
                    .HasColumnName("interception_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.KickDistance).HasColumnName("kick_distance");

                entity.Property(e => e.KickerPlayerId)
                    .HasColumnName("kicker_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.KickerPlayerName)
                    .HasColumnName("kicker_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.KickoffAttempt)
                    .HasColumnName("kickoff_attempt")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.KickoffReturnerPlayerId)
                    .HasColumnName("kickoff_returner_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.KickoffReturnerPlayerName)
                    .HasColumnName("kickoff_returner_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NoHuddle)
                    .HasColumnName("no_huddle")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.OwnKickoffRecovery)
                    .HasColumnName("own_kickoff_recovery")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.OwnKickoffRecoveryTd)
                    .HasColumnName("own_kickoff_recovery_td")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.PassAttempt)
                    .HasColumnName("pass_attempt")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.PassDefense1PlayerId)
                    .HasColumnName("pass_defense_1_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PassDefense1PlayerName)
                    .HasColumnName("pass_defense_1_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PassDefense2PlayerId)
                    .HasColumnName("pass_defense_2_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PassDefense2PlayerName)
                    .HasColumnName("pass_defense_2_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PassLength)
                    .HasColumnName("pass_length")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.PassLocation)
                    .HasColumnName("pass_location")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.PassTouchdown)
                    .HasColumnName("pass_touchdown")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.PasserPlayerId)
                    .HasColumnName("passer_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PasserPlayerName)
                    .HasColumnName("passer_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Penalty)
                    .HasColumnName("penalty")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.PenaltyPlayerId)
                    .HasColumnName("penalty_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PenaltyPlayerName)
                    .HasColumnName("penalty_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PenaltyTeam)
                    .HasColumnName("penalty_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.PenaltyType)
                    .HasColumnName("penalty_type")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PenaltyYards).HasColumnName("penalty_yards");

                entity.Property(e => e.PlayId).HasColumnName("play_id");

                entity.Property(e => e.PlayType)
                    .HasColumnName("play_type")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Posteam)
                    .IsRequired()
                    .HasColumnName("posteam")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.PosteamScore).HasColumnName("posteam_score");

                entity.Property(e => e.PosteamTimeoutsRemaining).HasColumnName("posteam_timeouts_remaining");

                entity.Property(e => e.PosteamType)
                    .IsRequired()
                    .HasColumnName("posteam_type")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.PuntAttempt)
                    .HasColumnName("punt_attempt")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.PuntBlocked)
                    .HasColumnName("punt_blocked")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.PuntReturnerPlayerId)
                    .HasColumnName("punt_returner_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PuntReturnerPlayerName)
                    .HasColumnName("punt_returner_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PunterPlayerId)
                    .HasColumnName("punter_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PunterPlayerName)
                    .HasColumnName("punter_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.QbDropback)
                    .HasColumnName("qb_dropback")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.QbHit)
                    .HasColumnName("qb_hit")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.QbKneel)
                    .HasColumnName("qb_kneel")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.QbScramble)
                    .HasColumnName("qb_scramble")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.QbSpike)
                    .HasColumnName("qb_spike")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Qtr).HasColumnName("qtr");

                entity.Property(e => e.QuarterEnd)
                    .HasColumnName("quarter_end")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.QuarterSecondsRemaining).HasColumnName("quarter_seconds_remaining");

                entity.Property(e => e.ReceiverPlayerId)
                    .HasColumnName("receiver_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiverPlayerName)
                    .HasColumnName("receiver_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ReplayOrChallenge)
                    .HasColumnName("replay_or_challenge")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.ReplayOrChallengeResult)
                    .HasColumnName("replay_or_challenge_result")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ReturnTeam)
                    .HasColumnName("return_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ReturnTouchdown)
                    .HasColumnName("return_touchdown")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.ReturnYards).HasColumnName("return_yards");

                entity.Property(e => e.RunGap)
                    .HasColumnName("run_gap")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.RunLocation)
                    .HasColumnName("run_location")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.RushAttempt)
                    .HasColumnName("rush_attempt")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.RushTouchdown)
                    .HasColumnName("rush_touchdown")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.RusherPlayerId)
                    .HasColumnName("rusher_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RusherPlayerName)
                    .HasColumnName("rusher_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Sack)
                    .HasColumnName("sack")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Safety)
                    .HasColumnName("safety")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Shotgun)
                    .HasColumnName("shotgun")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.SideOfField)
                    .IsRequired()
                    .HasColumnName("side_of_field")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SoloTackle)
                    .HasColumnName("solo_tackle")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.SoloTackle1PlayerId)
                    .HasColumnName("solo_tackle_1_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SoloTackle1PlayerName)
                    .HasColumnName("solo_tackle_1_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SoloTackle1Team)
                    .HasColumnName("solo_tackle_1_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SoloTackle2PlayerId)
                    .HasColumnName("solo_tackle_2_player_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SoloTackle2PlayerName)
                    .HasColumnName("solo_tackle_2_player_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SoloTackle2Team)
                    .HasColumnName("solo_tackle_2_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Sp)
                    .HasColumnName("sp")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.TackleForLoss)
                    .HasColumnName("tackle_for_loss")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.TdTeam)
                    .HasColumnName("td_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ThirdDownConverted)
                    .HasColumnName("third_down_converted")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.ThirdDownFailed)
                    .HasColumnName("third_down_failed")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Timeout)
                    .HasColumnName("timeout")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.TimeoutTeam)
                    .HasColumnName("timeout_team")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAwayScore).HasColumnName("total_away_score");

                entity.Property(e => e.TotalHomeScore).HasColumnName("total_home_score");

                entity.Property(e => e.Touchback)
                    .HasColumnName("touchback")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Touchdown)
                    .HasColumnName("touchdown")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.TwoPointAttempt)
                    .HasColumnName("two_point_attempt")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.TwoPointConvResult)
                    .HasColumnName("two_point_conv_result")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Week).HasColumnName("week");

                entity.Property(e => e.Yardline100).HasColumnName("yardline_100");

                entity.Property(e => e.YardsAfterCatch).HasColumnName("yards_after_catch");

                entity.Property(e => e.YardsGained).HasColumnName("yards_gained");

                entity.Property(e => e.Ydsnet).HasColumnName("ydsnet");

                entity.Property(e => e.Ydstogo).HasColumnName("ydstogo");

                entity.Property(e => e.Yrdln)
                    .HasColumnName("yrdln")
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GameEntity>(entity => {
                entity.ToTable("game", "footballdb");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Week)
                    .HasColumnName("week");

                entity.Property(e => e.HomeTeam)
                    .IsRequired()
                    .HasColumnName("home_team")
                    .HasMaxLength(3);

                entity.Property(e => e.AwayTeam)
                    .IsRequired()
                    .HasColumnName("away_team")
                    .HasMaxLength(3);
            });

            modelBuilder.Entity<StatEntity>(entity => {
                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Team).HasColumnName("team");

                entity.Property(e => e.AirYards).HasColumnName("air_yards");
            });
        }
    }
}
