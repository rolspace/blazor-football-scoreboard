using AutoMapper;
using Football.Application.Common.Mappings;
using Football.Domain.Entities;

namespace Football.Application.Common.Models;

public interface IPlayStats
{
    OffenseStats? OffenseStats { get; set; }

    DefenseStats? DefenseStats { get; set; }

    SpecialTeamsReceivingStats? SpecialTeamsReceivingStats { get; set; }

    SpecialTeamsKickingStats? SpecialTeamsKickingStats { get; set; }
}

public class PlayStats : MapFrom<Play>, IPlayStats
{
    public OffenseStats? OffenseStats { get; set; }

    public DefenseStats? DefenseStats { get; set; }

    public SpecialTeamsReceivingStats? SpecialTeamsReceivingStats { get; set; }

    public SpecialTeamsKickingStats? SpecialTeamsKickingStats { get; set; }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Play, PlayStats>()
            .ForMember(d => d.OffenseStats, o => o.MapFrom(s => s));
    }
}

public class OffenseStats : MapFrom<Play>
{
    public int AirYards { get; set; }
}

public class DefenseStats
{
    public int Sacks { get; set; }
}

public class SpecialTeamsReceivingStats
{
    public int ReturnYards { get; set; }
}

public class SpecialTeamsKickingStats
{
    public int Punts { get; set; }
}
