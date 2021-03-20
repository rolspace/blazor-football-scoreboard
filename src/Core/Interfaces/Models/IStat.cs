namespace Football.Core.Interfaces.Models
{
    public interface IStat
    {
        int GameId { get; set; }

        string Team { get; set; }

        int AirYards { get; set; }
    }
}
