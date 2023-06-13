using System.Linq.Expressions;
using AutoMapper;
using Football.Domain.Entities;

namespace Football.Application.Models.Mappings;

public class MapFromEntityProfile : Profile
{
    public MapFromEntityProfile()
    {
        // TODO: find a way to improve this with projection specific behavior, for now it does the job
        CreateProjection<Game, GameDto>()
            .ForMember(d => d.HomeScore,
                o => o.MapFrom(g => g.Stats.SingleOrDefault(s => s.Team == g.HomeTeam) != null
                    ? g.Stats.SingleOrDefault(s => s.Team == g.HomeTeam)!.Score
                    : 0))
            .ForMember(d => d.AwayScore,
                o => o.MapFrom(g => g.Stats.SingleOrDefault(s => s.Team == g.AwayTeam) != null
                    ? g.Stats.SingleOrDefault(s => s.Team == g.AwayTeam)!.Score
                    : 0))
            .ForMember(d => d.Quarter, o => o.NullSubstitute(1))
            .ForMember(d => d.QuarterSecondsRemaining, o => o.NullSubstitute(900));

        // TODO: verify that this projection generates a good query, for now it does the job
        CreateProjection<Play, PlayDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.PlayId))
            .ForMember(d => d.GameId, o => o.MapFrom(s => s.GameId))
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

        CreateProjection<Stat, StatDto>();
    }
}
