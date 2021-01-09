using Football.Core.Interfaces;

namespace Football.Core.Models
{
    public class Play : IPlay
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public string HomeTeam { get; set; }

        public int HomeScore { get; set; }

        public string AwayTeam { get; set; }

        public int AwayScore { get; set; }

        public int Quarter { get; set; }

        public int QuarterSecondsRemaining { get; set; }

        public string Description { get; set; }

        public Play()
        {
        }
    }
}
