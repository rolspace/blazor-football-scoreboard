using AutoMapper;
using Football.Application.Features.Plays.Models;

namespace Football.Application.Features.Stats.Mappings;

public class MapFromModelProfile : Profile
{
    public MapFromModelProfile()
    {
        CreateMap<PlayDto, SaveGameStatsCommand>()
            .ForMember(d => d.SaveGameStatCommandItems, o => o.MapFrom(s =>
                new List<SaveGameStatsCommandItem>()
                {
                    new SaveGameStatsCommandItem() {
                        Team = s.HomeTeam,
                        Score = s.HomeScore,
                        PassingYards = s.HomeTeamOnOffense && s.PlayType == "pass" ? s.YardsGained : 0,
                        Sacks = s.AwayTeamOnOffense && Convert.ToBoolean(s.Sack) ? 1 : 0,
                        ReturnYards = s.Kickoff && s.HomeTeamPossession && s.ReturnYards != null ? (int)s.ReturnYards : 0,
                        Punts = s.Punt && s.HomeTeamPossession && Convert.ToBoolean(s.PuntAttempt) ? 1 : 0
                    },
                    new SaveGameStatsCommandItem() {
                        Team = s.AwayTeam,
                        Score = s.AwayScore,
                        PassingYards = s.AwayTeamOnOffense && s.PlayType == "pass" ? s.YardsGained : 0,
                        Sacks = s.HomeTeamOnOffense && Convert.ToBoolean(s.Sack) ? 1 : 0,
                        ReturnYards = s.Kickoff && !s.HomeTeamPossession && s.ReturnYards != null ? (int)s.ReturnYards : 0,
                        Punts = s.Punt && !s.HomeTeamPossession && Convert.ToBoolean(s.PuntAttempt) ? 1 : 0
                    }
                }
            )
        );
    }
}
