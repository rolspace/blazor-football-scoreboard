//using System;

//namespace Core.Entities
//{
//    public class GameState
//    {
//        public Game Game { get; set; }

//        public int Quarter { get; }

//        public int HomeScore { get; }

//        public int AwayScore { get; }

//        public string QuarterTimeRemaining { get; }

//        public string PlayDescription { get; }

//        public GameState() { }

//        public GameState(Game game, int quarter, int quarterSecondsRemaining, int homeScore, int awayScore, string playDescription)
//        {
//            Game = game;
//            Quarter = quarter;
//            QuarterTimeRemaining = TimeSpan.FromSeconds(quarterSecondsRemaining).ToString("{0:mm\\:ss}");
//            HomeScore = homeScore;
//            AwayScore = awayScore;
//            PlayDescription = playDescription;
//        }

//        public override string ToString()
//        {
//            return string.Format("{0}: Home: {1} - Away {2}; {3}", Game.Id, HomeScore, AwayScore, PlayDescription);
//        }
//    }
//}
