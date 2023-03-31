using AutoMapper;
using Football.Application.Common.Mappings;
using Football.Domain.Entities;

namespace Football.Application.Common.Models;

public sealed class PlayDto : MapFrom<Play>
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public string HomeTeam { get; set; } = string.Empty;

    public int HomeScore { get; set; }

    public string AwayTeam { get; set; } = string.Empty;

    public int AwayScore { get; set; }

    public int Quarter { get; set; }

    public int QuarterSecondsRemaining { get; set; }

    public bool GameOver { get; set; }

    public string Description { get; set; } = string.Empty;

    public bool Kickoff { get; set; }

    public bool Punt { get; set; }

    public bool HomeTeamPossession { get; set; }

    public bool HomeTeamOnOffense { get; set; }

    public bool AwayTeamOnOffense { get; set; }

    public int YardsGained { get; set; }

    public int Sacks { get; set; }

    public int ReturnYards { get; set; }

    public int Punts { get; set; }

    public override string ToString()
    {
        return $"{AwayTeam}:{AwayScore} @ {HomeTeam}:{HomeScore} - {Description}";
    }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Play, PlayDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.PlayId))
            .ForMember(d => d.HomeScore, o => o.MapFrom(s => s.TotalHomeScore))
            .ForMember(d => d.AwayScore, o => o.MapFrom(s => s.TotalAwayScore))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Desc))
            .ForMember(d => d.Quarter, o => o.MapFrom(s => s.Qtr))
            .ForMember(d => d.GameOver, o => o.MapFrom(s => s.Desc == "END GAME"));
    }
}
