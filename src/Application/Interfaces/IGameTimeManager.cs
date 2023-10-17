namespace Football.Application.Interfaces;
public interface IGameTimeManager
{
    int GetQuarter();

    int GetQuarterSecondsRemaining();

    void SetTime();

    void IncrementFinishedGames(int count);
}
