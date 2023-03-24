using AutoMapper;
using Football.Application.Common.Mappings;
using Football.Domain.Entities;

namespace Football.Application.Common.Models;

public sealed class PlayDto : MapFrom<Play>
{
    public int Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public string HomeTeam { get; set; } = string.Empty;

    public int HomeScore { get; set; }

    public string AwayTeam { get; set; } = string.Empty;

    public int AwayScore { get; set; }

    public int Quarter { get; set; }

    public int QuarterSecondsRemaining { get; set; }

    public bool GameEnded { get; set; }

    public PlayLogDto? HomeTeamLog { get; set; }

    public PlayLogDto? AwayTeamLog { get; set; }

    public override string ToString()
    {
        return $"{AwayTeam}:{AwayScore} @ {HomeTeam}:{HomeScore} - {Description}";
    }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Play, PlayDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.PlayId))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Desc))
            .ForMember(d => d.HomeTeam, o => o.MapFrom(s => s.HomeTeam))
            .ForMember(d => d.HomeScore, o => o.MapFrom(s => s.TotalHomeScore))
            .ForMember(d => d.AwayTeam, o => o.MapFrom(s => s.AwayTeam))
            .ForMember(d => d.AwayScore, o => o.MapFrom(s => s.TotalAwayScore))
            .ForMember(d => d.Quarter, o => o.MapFrom(s => s.Qtr))
            .ForMember(d => d.QuarterSecondsRemaining, o => o.MapFrom(s => s.QuarterSecondsRemaining))
            .ForMember(d => d.GameEnded, o => o.MapFrom(s => s.Desc == "END GAME"))
            .ForMember(d => d.HomeTeamLog, o => o.ConvertUsing(new PlayLogDtoConverter(true)))
            .ForMember(d => d.AwayTeamLog, o => o.ConvertUsing(new PlayLogDtoConverter(false)));
    }
}
