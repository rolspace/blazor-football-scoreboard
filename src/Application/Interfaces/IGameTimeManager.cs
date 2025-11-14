namespace Football.Application.Interfaces;
public interface IGameTimeManager
{
    int GamesScheduled { get; set; }

    bool GamesOver { get; }

    int GetQuarter();

    int GetQuarterSecondsRemaining();

    void AdvanceTime();

    void IncrementGamesFinished(int count);
}
