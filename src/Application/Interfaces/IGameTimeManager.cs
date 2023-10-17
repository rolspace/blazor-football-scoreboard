namespace Football.Application.Interfaces;
public interface IGameTimeManager
{
    int GamesScheduled { get; set; }

    bool GameTimeOver { get; }

    int GetQuarter();

    int GetQuarterSecondsRemaining();

    void SetTime();

    void IncrementGamesFinished(int count);
}
