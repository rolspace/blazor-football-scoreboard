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
    }
}
