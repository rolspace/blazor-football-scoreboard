using AutoMapper;
using Football.Application.Common.Mappings;
using Football.Domain.Entities;

namespace Football.Application.Common.Models;

public sealed class PlayDto : MapFrom<Play>
{
    public int Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public int Quarter { get; set; }

    public int QuarterSecondsRemaining { get; set; }

    public bool GameEnded { get; set; }

    public HomePlayLog? HomeTeamLog { get; set; }

    public AwayPlayLog? AwayTeamLog { get; set; }

    public override string ToString()
    {
        return $"{AwayTeamLog?.Team}:{AwayTeamLog?.Score} @ {HomeTeamLog?.Team}:{HomeTeamLog?.Score} - {Description}";
    }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Play, PlayDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.PlayId))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Desc))
            .ForMember(d => d.Quarter, o => o.MapFrom(s => s.Qtr))
            .ForMember(d => d.GameEnded, o => o.MapFrom(s => s.Desc == "END GAME"))
            .ForMember(d => d.HomeTeamLog, o => o.MapFrom(s => s))
            .ForMember(d => d.AwayTeamLog, o => o.MapFrom(s => s));
    }
}
