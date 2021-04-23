using Football.Core.Interfaces.Models;

namespace Football.Core.Models
{
    public class Play : IPlay
    {
        public int Id { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public int Quarter { get; set; }

        public int QuarterSecondsRemaining { get; set; }

        public string Description { get; set; }

        public IGame Game { get; set; }

        public IPlayLog HomePlayLog { get; set; }

        public IPlayLog AwayPlayLog { get; set; }

        public override string ToString()
        {
            return $"{Game.Id} - {Game.HomeTeam}:{HomeScore} - {Game.AwayTeam}:{AwayScore} - {Description}";
        }
    }

    public class PlayLog : IPlayLog
    {
        public IOffensePlayLog OffensePlayLog { get; set; }

        public IDefensePlayLog DefensePlayLog { get; set; }

        public ISpecialPlayLog SpecialPlayLog { get; set; }
    }

    public class OffensePlayLog : IOffensePlayLog
    {
        public int AirYards { get; set; }
    }

    public class DefensePlayLog : IDefensePlayLog
    {
        public int Sacks { get; set; }
    }

    public class SpecialPlayLog : ISpecialPlayLog
    {
        public int ReturnYards { get; set; }

        public int Punts { get; set; }
    }
}
