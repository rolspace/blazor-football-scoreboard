using System.Collections.Generic;

namespace Football.Domain.Entities
{
    public class Game
    {
        public int Id { get; set; }

        public int Week { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public Time Time { get; set; }

        public IList<Play> Plays { get; private set; } = new List<Play>();

        public IList<Stat> Stats { get; private set; } = new List<Stat>();
    }
}
