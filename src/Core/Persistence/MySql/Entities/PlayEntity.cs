﻿namespace Football.Core.Persistence.MySql.Entities
{
    public class PlayEntity
    {
        public virtual GameEntity Game { get; set; }

        public int Id { get; set; }

        public int Week { get; set; }

        public int PlayId { get; set; }

        public int GameId { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string Posteam { get; set; }

        public string PosteamType { get; set; }

        public string Defteam { get; set; }

        public string SideOfField { get; set; }

        public int? Yardline100 { get; set; }

        public string GameDate { get; set; }

        public int? QuarterSecondsRemaining { get; set; }

        public int? HalfSecondsRemaining { get; set; }

        public int? GameSecondsRemaining { get; set; }

        public string GameHalf { get; set; }

        public byte? QuarterEnd { get; set; }

        public int Drive { get; set; }

        public byte? Sp { get; set; }

        public int Qtr { get; set; }

        public int? Down { get; set; }

        public int? GoalToGo { get; set; }

        public string Time { get; set; }

        public string Yrdln { get; set; }

        public int Ydstogo { get; set; }

        public int Ydsnet { get; set; }

        public string Desc { get; set; }

        public string PlayType { get; set; }

        public int YardsGained { get; set; }

        public byte? Shotgun { get; set; }

        public byte? NoHuddle { get; set; }

        public byte? QbDropback { get; set; }

        public byte? QbKneel { get; set; }

        public byte? QbSpike { get; set; }

        public byte? QbScramble { get; set; }

        public string PassLength { get; set; }

        public string PassLocation { get; set; }

        public int? AirYards { get; set; }

        public int? YardsAfterCatch { get; set; }

        public string RunLocation { get; set; }

        public string RunGap { get; set; }

        public string FieldGoalResult { get; set; }

        public int? KickDistance { get; set; }

        public string ExtraPointResult { get; set; }

        public string TwoPointConvResult { get; set; }

        public int HomeTimeoutsRemaining { get; set; }

        public int AwayTimeoutsRemaining { get; set; }

        public byte? Timeout { get; set; }

        public string TimeoutTeam { get; set; }

        public string TdTeam { get; set; }

        public int? PosteamTimeoutsRemaining { get; set; }

        public int? DefteamTimeoutsRemaining { get; set; }

        public int? TotalHomeScore { get; set; }

        public int? TotalAwayScore { get; set; }

        public int? PosteamScore { get; set; }

        public int? DefteamScore { get; set; }

        public byte? PuntBlocked { get; set; }

        public byte? FirstDownRush { get; set; }

        public byte? FirstDownPass { get; set; }

        public byte? FirstDownPenalty { get; set; }

        public byte? ThirdDownConverted { get; set; }

        public byte? ThirdDownFailed { get; set; }

        public byte? FourthDownConverted { get; set; }

        public byte? FourthDownFailed { get; set; }

        public byte? IncompletePass { get; set; }

        public byte? Touchback { get; set; }

        public byte? Interception { get; set; }

        public byte? FumbleForced { get; set; }

        public byte? FumbleNotForced { get; set; }

        public byte? FumbleOutOfBounds { get; set; }

        public byte? SoloTackle { get; set; }

        public byte? Safety { get; set; }

        public byte? Penalty { get; set; }

        public byte? TackleForLoss { get; set; }

        public byte? FumbleLost { get; set; }

        public byte? OwnKickoffRecovery { get; set; }

        public byte? OwnKickoffRecoveryTd { get; set; }

        public byte? QbHit { get; set; }

        public byte? RushAttempt { get; set; }

        public byte? PassAttempt { get; set; }

        public byte? Sack { get; set; }

        public byte? Touchdown { get; set; }

        public byte? RushTouchdown { get; set; }

        public byte? PassTouchdown { get; set; }

        public byte? ReturnTouchdown { get; set; }

        public byte? ExtraPointAttempt { get; set; }

        public byte? TwoPointAttempt { get; set; }

        public byte? FieldGoalAttempt { get; set; }

        public byte? KickoffAttempt { get; set; }

        public byte? PuntAttempt { get; set; }

        public byte? Fumble { get; set; }

        public byte? CompletePass { get; set; }

        public byte? AssistTackle { get; set; }

        public string PasserPlayerId { get; set; }

        public string PasserPlayerName { get; set; }

        public string ReceiverPlayerId { get; set; }

        public string ReceiverPlayerName { get; set; }

        public string RusherPlayerId { get; set; }

        public string RusherPlayerName { get; set; }

        public string InterceptionPlayerId { get; set; }

        public string InterceptionPlayerName { get; set; }

        public string PuntReturnerPlayerId { get; set; }

        public string PuntReturnerPlayerName { get; set; }

        public string KickoffReturnerPlayerName { get; set; }

        public string KickoffReturnerPlayerId { get; set; }

        public string PunterPlayerId { get; set; }

        public string PunterPlayerName { get; set; }

        public string KickerPlayerId { get; set; }

        public string KickerPlayerName { get; set; }

        public string ForcedFumblePlayer1Team { get; set; }

        public string ForcedFumblePlayer1PlayerId { get; set; }

        public string ForcedFumblePlayer1PlayerName { get; set; }

        public string ForcedFumblePlayer2PlayerId { get; set; }

        public string ForcedFumblePlayer2Team { get; set; }

        public string ForcedFumblePlayer2PlayerName { get; set; }

        public string SoloTackle1Team { get; set; }

        public string SoloTackle2Team { get; set; }

        public string SoloTackle1PlayerId { get; set; }

        public string SoloTackle2PlayerId { get; set; }

        public string SoloTackle1PlayerName { get; set; }

        public string SoloTackle2PlayerName { get; set; }

        public string PassDefense1PlayerId { get; set; }

        public string PassDefense1PlayerName { get; set; }

        public string PassDefense2PlayerId { get; set; }

        public string PassDefense2PlayerName { get; set; }

        public string Fumbled1Team { get; set; }

        public string Fumbled1PlayerId { get; set; }

        public string Fumbled1PlayerName { get; set; }

        public string Fumbled2PlayerId { get; set; }

        public string Fumbled2PlayerName { get; set; }

        public string Fumbled2Team { get; set; }

        public string FumbleRecovery1Team { get; set; }

        public int? FumbleRecovery1Yards { get; set; }

        public string FumbleRecovery1PlayerId { get; set; }

        public string FumbleRecovery1PlayerName { get; set; }

        public string FumbleRecovery2Team { get; set; }

        public int? FumbleRecovery2Yards { get; set; }

        public string FumbleRecovery2PlayerId { get; set; }

        public string FumbleRecovery2PlayerName { get; set; }

        public string ReturnTeam { get; set; }

        public int? ReturnYards { get; set; }

        public string PenaltyTeam { get; set; }

        public string PenaltyPlayerId { get; set; }

        public string PenaltyPlayerName { get; set; }

        public int? PenaltyYards { get; set; }

        public byte? ReplayOrChallenge { get; set; }

        public string ReplayOrChallengeResult { get; set; }

        public string PenaltyType { get; set; }

        public byte? DefensiveTwoPointAttempt { get; set; }

        public byte? DefensiveTwoPointConv { get; set; }

        public byte? DefensiveExtraPointAttempt { get; set; }

        public byte? DefensiveExtraPointConv { get; set; }
    }
}