using System.Collections.Generic;

namespace Football.Domain.Entities
{
    public class Game
    {
        public int Id { get; set; }

        public int Week { get; set; }

        public string HomeTeam { get; set; } = string.Empty;

        public string AwayTeam { get; set; } = string.Empty;

        public GameState? State { get; set; }

        public int? Quarter { get; set; }

        public int? QuarterSecondsRemaining { get; set; }

        public IList<Play> Plays { get; private set; } = new List<Play>();

        public IList<Stat> Stats { get; private set; } = new List<Stat>();
    }
}
