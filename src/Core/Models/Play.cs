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

        public PlayStats PlayStats { get; set; }

        public override string ToString()
        {
            return $"{Game.Id} - {Game.HomeTeam}:{HomeScore} - {Game.AwayTeam}:{AwayScore} - {Description}";
        }
    }

    public class PlayStats
    {
        public TeamStats Home { get; set; }
        public TeamStats Away { get; set; }
    }

    public class TeamStats
    {
        public string Team { get; set; }
        public int Score { get; set; }
        public OffenseStats OffenseStats { get; set; }
        public DefenseStats DefenseStats { get; set; }
        public SpecialStats SpecialStats { get; set; }
    }

    public class OffenseStats
    {
        public int AirYards { get; set; }
    }

    public class DefenseStats
    {
        public int Sacks { get; set; }
    }

    public class SpecialStats
    {
        public int Punts { get; set; }
    }
}
