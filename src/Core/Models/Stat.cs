using Football.Core.Interfaces.Models;

namespace Football.Core.Models
{
    public class Stat : IStat
    {
        public int GameId { get; set; }

        public string Team { get; set; }

        public int AirYards { get; set; }
    }
}
