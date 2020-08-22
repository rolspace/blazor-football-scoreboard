namespace Core.Entities
{
    public class GameState
    {
        public Game Game { get; set; }

        public string PlayDescription { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public GameState() { }

        public GameState(Game game, string playDescription = "", int homeScore = 0, int awayScore = 0)
        {
            Game = game;
            PlayDescription = playDescription;
            HomeScore = homeScore;
            AwayScore = awayScore;
        }

        public override string ToString()
        {
            return string.Format("{0}: Home: {1} - Away {2}; {3}", Game.Id, HomeScore, AwayScore, PlayDescription);
        }
    }
}
