using System;

namespace Scoreboard.Client.ViewModels
{
    public class GameState
    {
        public int Id { get; set; }

        public int Week { get; set; }

        public string HomeTeam { get; set; } = "";

        public int HomeScore { get; set; }

        public string AwayTeam { get; set; } = "";

        public int AwayScore { get; set; }

        public int Quarter { get; set; }

        public int QuarterSecondsRemaining { get; set; }

        public string QuarterTimeRemaining => TimeSpan.FromSeconds(QuarterSecondsRemaining).ToString(@"mm\:ss");

        public string PlayDescription { get; set; } = "";

        public GameState() { }

        public GameState(int id, int week, string homeTeam, int homeScore, string awayTeam, int awayScore,
            int quarter, int quarterSecondsRemaining, string description)
        {
            Id = id;
            Week = week;
            HomeTeam = homeTeam;
            HomeScore = homeScore;
            AwayTeam = awayTeam;
            AwayScore = awayScore;
            Quarter = quarter;
            QuarterSecondsRemaining = quarterSecondsRemaining;
            PlayDescription = description;
        }

        public override string ToString()
        {
            return string.Format("{0}: Home: {1} - Away {2}; {3}", Id, HomeScore, AwayScore, PlayDescription);
        }
    }
}
