namespace Core.Shared
{
    public class GamePlay
    {
        public int GameId { get; set; }

        public string Desc { get; set; }

        public int? HomeScore { get; set; }

        public int? AwayScore { get; set; }

        public GamePlay() { }

        public GamePlay(int gameId, string desc, int? homeScore, int? awayScore)
        {
            GameId = gameId;
            Desc = desc;
            HomeScore = homeScore;
            AwayScore = awayScore;
        }

        public override string ToString()
        {
            return string.Format("{0}: Home: {1} - Away {2}; {3}", GameId, HomeScore, AwayScore, Desc);
        }
    }
}
