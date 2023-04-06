namespace Football.Application.Models;

public sealed class PlayDto
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public string HomeTeam { get; set; } = string.Empty;

    public int HomeScore { get; set; }

    public string AwayTeam { get; set; } = string.Empty;

    public int AwayScore { get; set; }

    public int Quarter { get; set; }

    public int QuarterSecondsRemaining { get; set; }

    public bool GameOver { get; set; }

    public string Description { get; set; } = string.Empty;

    public bool Kickoff { get; set; }

    public bool Punt { get; set; }

    public bool HomeTeamPossession { get; set; }

    public bool HomeTeamOnOffense { get; set; }

    public bool AwayTeamOnOffense { get; set; }

    public int YardsGained { get; set; }

    public bool? Sack { get; set; }

    public int? ReturnYards { get; set; }

    public bool? PuntAttempt { get; set; }

    public override string ToString()
    {
        return $"{GameId} - {AwayTeam}:{AwayScore} @ {HomeTeam}:{HomeScore} - {Description}";
    }
}
