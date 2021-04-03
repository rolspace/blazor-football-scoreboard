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

        IPlayData HomePlayData { get; set; }

        IPlayData AwayPlayData { get; set; }
    }

    public interface IPlayData
    {
        IOffensivePlayData OffensivePlayData { get; set; }

        IDefensivePlayData DefensivePlayData { get; set; }

        ISpecialPlayData SpecialPlayData { get; set; }
    }

    public interface IOffensivePlayData
    {
        int AirYards { get; set; }
    }

    public interface IDefensivePlayData
    {
        int Sacks { get; set; }
    }

    public interface ISpecialPlayData
    {
        int Punts { get; set; }
    }
}
