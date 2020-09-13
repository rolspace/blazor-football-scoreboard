using System;

namespace Core.Entities
{
    public class GameState
    {
        public Game Game { get; set; }

        public int Quarter { get; set; }

        public string QuarterTimeRemaining { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public string PlayDescription { get; set; }

        public GameState() { }

        public GameState(Game game, int quarter = 0, int quarterSecondsRemaining = 900,
            int homeScore = 0, int awayScore = 0, string playDescription = "")
        {
            Game = game;
            Quarter = quarter;
            QuarterTimeRemaining = TimeSpan.FromSeconds(quarterSecondsRemaining).ToString("{0:mm\\:ss}");
            HomeScore = homeScore;
            AwayScore = awayScore;
            PlayDescription = playDescription;
        }

        public override string ToString()
        {
            return string.Format("{0}: Home: {1} - Away {2}; {3}", Game.Id, HomeScore, AwayScore, PlayDescription);
        }
    }
}
