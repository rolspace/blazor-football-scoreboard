using AutoMapper;
using Football.Application.Features.Stats.Models;
using Football.Domain.Entities;

namespace Football.Application.Features.Stats.Mappings;

public class MapFromEntityProfile : Profile
{
    public MapFromEntityProfile()
    {
        // TODO: find a way to improve this with projection specific behavior, for now it does the job
        CreateProjection<Stat, StatDto>()
            .ForMember(d => d.Home, o => o.MapFrom(s => s.Game != null && s.Game.HomeTeam == s.Team));
    }
}
