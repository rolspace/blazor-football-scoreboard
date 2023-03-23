using AutoMapper;
using Football.Application.Common.Mappings;
using Football.Domain.Entities;

namespace Football.Application.Common.Models;

public class PlayLogDto
{
    public string Team { get; set; } = string.Empty;

    public int Score { get; set; }

    public OffensePlayLogDto? OffenseLog { get; set; }

    public DefensePlayLogDto? DefenseLog { get; set; }

    public SpecialTeamsPlayLogDto? SpecialTeamsLog { get; set; }
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

public class PlayLogDtoConverter : IValueConverter<Play, PlayLogDto>
{
    private bool _homeTeam;

    public PlayLogDtoConverter(bool homeTeam)
    {
        _homeTeam = homeTeam;
    }

    public PlayLogDto Convert(Play source, ResolutionContext context)
    {
        return new PlayLogDto()
        {
            Team = _homeTeam ? source.HomeTeam : source.AwayTeam,
            Score = (_homeTeam ? source.TotalHomeScore : source.TotalAwayScore) ?? 0
        };
    }
}
