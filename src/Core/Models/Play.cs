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

        public IPlayData HomePlayData { get; set; }

        public IPlayData AwayPlayData { get; set; }

        public override string ToString()
        {
            return $"{Game.Id} - {Game.HomeTeam}:{HomeScore} - {Game.AwayTeam}:{AwayScore} - {Description}";
        }
    }

    public class PlayData : IPlayData
    {
        public IOffensivePlayData OffensivePlayData { get; set; }

        public IDefensivePlayData DefensivePlayData { get; set; }

        public ISpecialPlayData SpecialPlayData { get; set; }
    }

    public class OffensivePlayData : IOffensivePlayData
    {
        public int AirYards { get; set; }
    }

    public class DefensivePlayData : IDefensivePlayData
    {
        public int Sacks { get; set; }
    }

    public class SpecialPlayData : ISpecialPlayData
    {
        public int ReturnYards { get; set; }

        public int Punts { get; set; }
    }
}
