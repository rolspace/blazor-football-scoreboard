namespace Football.Infrastructure.Options;

public class ScoreboardOptions
{
    public const string Key = "Scoreboard";

    /// <summary>
    /// Number of the week for the games schedule
    /// Should be a number from 1 to 17
    /// </summary>
    public int Week { get; set; } = 1;
}
