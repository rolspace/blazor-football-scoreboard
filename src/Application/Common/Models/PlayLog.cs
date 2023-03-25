using AutoMapper;
using Football.Application.Common.Mappings;
using Football.Domain.Entities;

namespace Football.Application.Common.Models;

public interface IPlayLog
{
    OffenseLog? OffenseLog { get; set; }

    DefenseLog? DefenseLog { get; set; }

    SpecialTeamsReceivingLog? SpecialTeamsReceivingLog { get; set; }

    SpecialTeamsKickingLog? SpecialTeamsKickingLog { get; set; }
}

public class PlayLog : MapFrom<Play>, IPlayLog
{
    public OffenseLog? OffenseLog { get; set; }

    public DefenseLog? DefenseLog { get; set; }

    public SpecialTeamsReceivingLog? SpecialTeamsReceivingLog { get; set; }

    public SpecialTeamsKickingLog? SpecialTeamsKickingLog { get; set; }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Play, PlayLog>()
            .ForMember(d => d.OffenseLog, o => o.MapFrom(s => s));
    }
}

public class OffenseLog : MapFrom<Play>
{
    public int AirYards { get; set; }
}

public class DefenseLog
{
    public int Sacks { get; set; }
}

public class SpecialTeamsReceivingLog
{
    public int ReturnYards { get; set; }
}

public class SpecialTeamsKickingLog
{
    public int Punts { get; set; }
}
