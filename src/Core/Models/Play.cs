namespace Football.Core.Models
{
    public class Play
    {
        public int Id { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public int Quarter { get; set; }

        public int QuarterSecondsRemaining { get; set; }

        public string Description { get; set; }

        public Game Game { get; set; }

        public PlayLog HomePlayLog { get; set; }

        public PlayLog AwayPlayLog { get; set; }

        public override string ToString()
        {
            return $"{Game.Id} - {Game.HomeTeam}:{HomeScore} - {Game.AwayTeam}:{AwayScore} - {Description}";
        }
    }

    public class PlayLog
    {
        public OffensePlayLog OffensePlayLog { get; set; }

        public DefensePlayLog DefensePlayLog { get; set; }

        public SpecialPlayLog SpecialPlayLog { get; set; }
    }

    public class OffensePlayLog
    {
        public int AirYards { get; set; }
    }

    public class DefensePlayLog
    {
        public int Sacks { get; set; }
    }

    public class SpecialPlayLog
    {
        public int ReturnYards { get; set; }

        public int Punts { get; set; }
    }
}
