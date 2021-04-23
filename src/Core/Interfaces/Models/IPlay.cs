namespace Football.Core.Interfaces.Models
{
    public interface IPlay
    {
        int Id { get; set; }

        int HomeScore { get; set; }

        int AwayScore { get; set; }

        int Quarter { get; set; }

        int QuarterSecondsRemaining { get; set; }

        string Description { get; set; }

        IGame Game { get; set; }

        IPlayLog HomePlayLog { get; set; }

        IPlayLog AwayPlayLog { get; set; }
    }

    public interface IPlayLog
    {
        IOffensePlayLog OffensePlayLog { get; set; }

        IDefensePlayLog DefensePlayLog { get; set; }

        ISpecialPlayLog SpecialPlayLog { get; set; }
    }

    public interface IOffensePlayLog
    {
        int AirYards { get; set; }
    }

    public interface IDefensePlayLog
    {
        int Sacks { get; set; }
    }

    public interface ISpecialPlayLog
    {
        int ReturnYards { get; set; }

        int Punts { get; set; }
    }
}
