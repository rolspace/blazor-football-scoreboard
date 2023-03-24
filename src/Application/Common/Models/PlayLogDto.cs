using AutoMapper;
using Football.Application.Common.Mappings;
using Football.Domain.Entities;

namespace Football.Application.Common.Models;

public class HomePlayLogDto : MapFrom<Play>, IPlayLogDto
{
    public string Team { get; set; } = string.Empty;

    public int Score { get; set; }

    public OffensePlayLogDto? OffenseLog { get; set; }

    public DefensePlayLogDto? DefenseLog { get; set; }

    public SpecialTeamsPlayLogDto? SpecialTeamsLog { get; set; }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Play, HomePlayLogDto>()
            .ForMember(d => d.Team, o => o.MapFrom(s => s.HomeTeam))
            .ForMember(d => d.Score, o => o.MapFrom(s => s.TotalHomeScore));
    }
}

public class AwayPlayLogDto : MapFrom<Play>, IPlayLogDto
{
    public string Team { get; set; } = string.Empty;

    public int Score { get; set; }

    public OffensePlayLogDto? OffenseLog { get; set; }

    public DefensePlayLogDto? DefenseLog { get; set; }

    public SpecialTeamsPlayLogDto? SpecialTeamsLog { get; set; }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Play, AwayPlayLogDto>()
            .ForMember(d => d.Team, o => o.MapFrom(s => s.AwayTeam))
            .ForMember(d => d.Score, o => o.MapFrom(s => s.TotalAwayScore));
    }
}
