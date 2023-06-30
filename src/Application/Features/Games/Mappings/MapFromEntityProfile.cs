using AutoMapper;
using Football.Application.Features.Games.Models;
using Football.Domain.Entities;

namespace Football.Application.Features.Games.Mappings;

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
    }
}
