namespace Football.Domain.Entities;

public class Play
{
    public int Id { get; set; }

    public int PlayId { get; set; }

    public int GameId { get; set; }

    public Game? Game { get; set; }

    public int Week { get; set; }

    public string HomeTeam { get; set; } = null!;

    public int HomeScore { get; set; }

    public string AwayTeam { get; set; } = null!;

    public int AwayScore { get; set; }

    public int Qtr { get; set; }

    public int? QuarterSecondsRemaining { get; set; }

    public int? HalfSecondsRemaining { get; set; }

    public int? GameSecondsRemaining { get; set; }

    public string GameDate { get; set; } = null!;

    public string GameHalf { get; set; } = null!;

    public string Desc { get; set; } = null!;

    public string Time { get; set; } = null!;

    public string SideOfField { get; set; } = null!;

    public string Yrdln { get; set; } = null!;

    public string? PlayType { get; set; }

    public int? Down { get; set; }

    public int? GoalToGo { get; set; }

    public int? Yardline100 { get; set; }

    public int Drive { get; set; }

    public int Ydstogo { get; set; }

    public bool Sp { get; set; }

    public bool QuarterEnd { get; set; }

    public bool Touchdown { get; set; }

    public bool Shotgun { get; set; }

    public bool NoHuddle { get; set; }

    public bool QbDropback { get; set; }

    public bool QbKneel { get; set; }

    public bool QbSpike { get; set; }

    public bool QbScramble { get; set; }

    public bool QbHit { get; set; }

    public bool FirstDownPass { get; set; }

    public bool FirstDownRush { get; set; }

    public bool FirstDownPenalty { get; set; }

    public bool ThirdDownConverted { get; set; }

    public bool ThirdDownFailed { get; set; }

    public bool FourthDownConverted { get; set; }

    public bool FourthDownFailed { get; set; }

    public bool PassAttempt { get; set; }

    public bool PassTouchdown { get; set; }

    public bool CompletePass { get; set; }

    public bool IncompletePass { get; set; }

    public bool Interception { get; set; }

    public bool RushAttempt { get; set; }

    public bool RushTouchdown { get; set; }

    public bool Fumble { get; set; }

    public bool FumbleForced { get; set; }

    public bool FumbleNotForced { get; set; }

    public bool FumbleLost { get; set; }

    public bool FumbleOutOfBounds { get; set; }

    public bool Touchback { get; set; }

    public bool ReturnTouchdown { get; set; }

    public bool PuntAttempt { get; set; }

    public bool PuntBlocked { get; set; }

    public bool KickoffAttempt { get; set; }

    public bool OwnKickoffRecovery { get; set; }

    public bool OwnKickoffRecoveryTd { get; set; }

    public bool ExtraPointAttempt { get; set; }

    public bool TwoPointAttempt { get; set; }

    public bool FieldGoalAttempt { get; set; }

    public bool SoloTackle { get; set; }

    public bool AssistTackle { get; set; }

    public bool TackleForLoss { get; set; }

    public bool Sack { get; set; }

    public bool Safety { get; set; }

    public bool Penalty { get; set; }

    public bool ReplayOrChallenge { get; set; }

    public bool DefensiveTwoPointAttempt { get; set; }

    public bool DefensiveTwoPointConv { get; set; }

    public bool DefensiveExtraPointAttempt { get; set; }

    public bool DefensiveExtraPointConv { get; set; }

    public string Posteam { get; set; } = null!;

    public string PosteamType { get; set; } = null!;

    public int? PosteamScore { get; set; }

    public string Defteam { get; set; } = null!;

    public int? DefteamScore { get; set; }

    public string? TdTeam { get; set; }

    public int Ydsnet { get; set; }

    public int YardsGained { get; set; }

    public string? PassLength { get; set; }

    public string? PassLocation { get; set; }

    public int? AirYards { get; set; }

    public int? YardsAfterCatch { get; set; }

    public string? RunLocation { get; set; }

    public string? RunGap { get; set; }

    public string? FieldGoalResult { get; set; }

    public string? ExtraPointResult { get; set; }

    public string? TwoPointConvResult { get; set; }

    public string? ReturnTeam { get; set; }

    public int? ReturnYards { get; set; }

    public int? KickDistance { get; set; }

    public string? PenaltyTeam { get; set; }

    public string? PenaltyType { get; set; }

    public int? PenaltyYards { get; set; }

    public string? ReplayOrChallengeResult { get; set; }

    public bool Timeout { get; set; }

    public string? TimeoutTeam { get; set; }

    public int HomeTimeoutsRemaining { get; set; }

    public int AwayTimeoutsRemaining { get; set; }

    public int? PosteamTimeoutsRemaining { get; set; }

    public int? DefteamTimeoutsRemaining { get; set; }
}
