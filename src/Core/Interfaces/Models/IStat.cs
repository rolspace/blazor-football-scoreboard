namespace Football.Core.Interfaces.Models
{
    public interface IStat
    {
        int GameId { get; set; }

        string Team { get; set; }

        int AirYards { get; set; }

        int Sacks { get; set; }

        int Punts { get; set; }

        int ReturnYards { get; set; }
    }
}
