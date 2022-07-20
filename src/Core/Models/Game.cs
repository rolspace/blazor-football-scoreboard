using System.Collections.Generic;

namespace Football.Core.Models
{
    public class Game
    {
        public int Id { get; set; }

        public int Week { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public Time Time { get; set; }

        public virtual ICollection<Play> Plays { get; set; }

        public virtual ICollection<Stat> Stats { get; set; }
    }

    public class Time
    {
        public int Quarter { get; set; }

        public int QuarterSecondsRemaining { get; set; }
    }
}
