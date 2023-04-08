using AutoMapper;
using Football.Application.Models;

namespace Football.Application.Stats.Commands.Mappings;

public class MapFromDtoProfile : Profile
{
    public MapFromDtoProfile()
    {
        CreateMap<PlayDto, SaveStatsCommand>()
            .ForMember(d => d.SaveStatCommandItems, o => o.MapFrom(s =>
                new List<SaveStatsCommandItem>()
                {
                    new SaveStatsCommandItem() {
                        Team = s.HomeTeam,
                        Score = s.HomeScore,
                        YardsGained = s.HomeTeamOnOffense ? s.YardsGained : 0,
                        Sacks = s.AwayTeamOnOffense && Convert.ToBoolean(s.Sack) ? 1 : 0,
                        ReturnYards = s.Kickoff && s.HomeTeamPossession && s.ReturnYards != null ? (int)s.ReturnYards : 0,
                        Punts = s.Punt && s.HomeTeamPossession && Convert.ToBoolean(s.PuntAttempt) ? 1 : 0
                    },
                    new SaveStatsCommandItem() {
                        Team = s.AwayTeam,
                        Score = s.AwayScore,
                        YardsGained = s.AwayTeamOnOffense ? s.YardsGained : 0,
                        Sacks = s.HomeTeamOnOffense && Convert.ToBoolean(s.Sack) ? 1 : 0,
                        ReturnYards = s.Kickoff && !s.HomeTeamPossession && s.ReturnYards != null ? (int)s.ReturnYards : 0,
                        Punts = s.Punt && !s.HomeTeamPossession && Convert.ToBoolean(s.PuntAttempt) ? 1 : 0
                    }
                }
            )
        );
    }
}
