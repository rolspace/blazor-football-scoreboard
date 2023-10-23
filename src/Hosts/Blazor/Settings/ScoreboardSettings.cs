namespace Football.Blazor.Settings;

public class ScoreboardSettings
{
    public const string Key = "Scoreboard";

    public int Week { get; set; } = 0;

    public string ApiBaseUrl { get; set; } = string.Empty;
}
