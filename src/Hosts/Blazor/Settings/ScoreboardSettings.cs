namespace Football.Blazor.Settings;

public class ScoreboardSettings
{
    public const string ScoreboardSection = "Scoreboard";

    public int Week { get; set; } = 1;

    public string ApiBaseUrl { get; set; } = string.Empty;

    public string HubUrl { get; set; } = string.Empty;
}
