using AutoMapper;
using Football.Application.Common.Mappings;
using Football.Domain.Entities;

namespace Football.Application.Common.Models;

public interface IPlayLog
{
    string Team { get; set; }

    int Score { get; set; }

    OffensePlayLog? OffenseLog { get; set; }

    DefensePlayLog? DefenseLog { get; set; }

    SpecialTeamsPlayLog? SpecialTeamsLog { get; set; }
}

public class HomePlayLog : MapFrom<Play>, IPlayLog
{
    public string Team { get; set; } = string.Empty;

    public int Score { get; set; }

    public OffensePlayLog? OffenseLog { get; set; }

    public DefensePlayLog? DefenseLog { get; set; }

    public SpecialTeamsPlayLog? SpecialTeamsLog { get; set; }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Play, HomePlayLog>()
            .ForMember(d => d.Team, o => o.MapFrom(s => s.HomeTeam))
            .ForMember(d => d.Score, o => o.MapFrom(s => s.TotalHomeScore));
    }
}

public class AwayPlayLog : MapFrom<Play>, IPlayLog
{
    public string Team { get; set; } = string.Empty;

    public int Score { get; set; }

    public OffensePlayLog? OffenseLog { get; set; }

    public DefensePlayLog? DefenseLog { get; set; }

    public SpecialTeamsPlayLog? SpecialTeamsLog { get; set; }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Play, AwayPlayLog>()
            .ForMember(d => d.Team, o => o.MapFrom(s => s.AwayTeam))
            .ForMember(d => d.Score, o => o.MapFrom(s => s.TotalAwayScore));
    }
}

public class OffensePlayLog
{
    public int AirYards { get; set; }
}

public class DefensePlayLog
{
    public int Sacks { get; set; }
}

public class SpecialTeamsPlayLog
{
    public int ReturnYards { get; set; }

    public int Punts { get; set; }
}
