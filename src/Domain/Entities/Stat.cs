namespace Football.Domain.Entities
{
    public class Stat
    {
        public int GameId { get; set; }

        public string Team { get; set; }

        public int Score { get; set; }

        public int AirYards { get; set; }

        public int Punts { get; set; }

        public int ReturnYards { get; set; }

        public int Sacks { get; set; }

        public virtual Game Game { get; set; }
    }
}