namespace Football.Application.Models;

public sealed class GameDto
{
    public int Id { get; set; }

    public int Week { get; set; }

    public string HomeTeam { get; set; } = string.Empty;

    public string AwayTeam { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public int Quarter { get; set; }

    public int QuarterSecondsRemaining { get; set; }
}
