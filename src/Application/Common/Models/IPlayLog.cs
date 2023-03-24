namespace Football.Application.Common.Models;

public interface IPlayLogDto
{
    string Team { get; set; }

    int Score { get; set; }

    OffensePlayLogDto? OffenseLog { get; set; }

    DefensePlayLogDto? DefenseLog { get; set; }

    SpecialTeamsPlayLogDto? SpecialTeamsLog { get; set; }
}

public class OffensePlayLogDto
{
    public int AirYards { get; set; }
}

public class DefensePlayLogDto
{
    public int Sacks { get; set; }
}

public class SpecialTeamsPlayLogDto
{
    public int ReturnYards { get; set; }

    public int Punts { get; set; }
}
