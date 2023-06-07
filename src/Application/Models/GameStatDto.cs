namespace Football.Application.Models;

public sealed class GameStatDto
{
    public int GameId { get; set; }

    public List<StatDto> Stats { get; set; } = new List<StatDto>();
}

public sealed class StatDto
{
    public string? Team { get; set; }

    public int Score { get; set; }

    public int PassingYards { get; set; }

    public int ReturnYards { get; set; }

    public int Sacks { get; set; }

    public int Punts { get; set; }
}
