using Football.Core.Interfaces;

namespace Football.Core.Models
{
    public class Game : IGame
    {
        public int Id { get; set; }

        public int Week { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public Game()
        {
        }
    }
}
