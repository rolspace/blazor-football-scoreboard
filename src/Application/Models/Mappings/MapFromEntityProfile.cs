using AutoMapper;
using Football.Domain.Entities;

namespace Football.Application.Models.Mappings;

public class MapFromEntityProfile : Profile
{
    public MapFromEntityProfile()
    {
        CreateMap<Game, GameDto>();

        CreateMap<Play, PlayDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.PlayId))
            .ForMember(d => d.HomeScore, o => o.MapFrom(s => s.TotalHomeScore))
            .ForMember(d => d.AwayScore, o => o.MapFrom(s => s.TotalAwayScore))
            .ForMember(d => d.Quarter, o => o.MapFrom(s => s.Qtr))
            .ForMember(d => d.GameOver, o => o.MapFrom(s => s.Desc == "END GAME"))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Desc))
            .ForMember(d => d.Kickoff, o => o.MapFrom(s => s.PlayType == "kickoff"))
            .ForMember(d => d.Punt, o => o.MapFrom(s => s.PlayType == "punt"))
            .ForMember(d => d.HomeTeamPossession, o => o.MapFrom(s => s.Posteam == s.HomeTeam))
            .ForMember(d => d.HomeTeamOnOffense, o => o.MapFrom(s => s.Posteam == s.HomeTeam && (s.PlayType != "kickoff" || s.PlayType != "punt")))
            .ForMember(d => d.AwayTeamOnOffense, o => o.MapFrom(s => s.Posteam == s.AwayTeam && (s.PlayType != "kickoff" || s.PlayType != "punt")));
    }
}
