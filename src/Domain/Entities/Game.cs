using Football.Domain.Enums;

namespace Football.Domain.Entities;

public class Game
{
    public int Id { get; set; }

    public int Week { get; set; }

    public string HomeTeam { get; set; } = null!;

    public string AwayTeam { get; set; } = null!;

    public GameState? State { get; set; }

    public int? Quarter { get; set; }

    public int? QuarterSecondsRemaining { get; set; }

    public virtual ICollection<Play> Plays { get; set; } = new List<Play>();

    public virtual ICollection<Stat> Stats { get; set; } = new List<Stat>();
}
