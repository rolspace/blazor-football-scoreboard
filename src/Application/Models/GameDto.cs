namespace Football.Application.Models;

// TODO: improve DTO definition
public sealed class GameDto
{
    public int Id { get; set; }

    public int Week { get; set; }

    public string HomeTeam { get; set; } = string.Empty;

    public int HomeScore { get; set; }

    public string AwayTeam { get; set; } = string.Empty;

    public int AwayScore { get; set; }

    public string State { get; set; } = string.Empty;

    public int Quarter { get; set; }

    public int QuarterSecondsRemaining { get; set; }

    public string QuarterTimeRemaining => TimeSpan.FromSeconds(QuarterSecondsRemaining).ToString(@"mm\:ss");
}
