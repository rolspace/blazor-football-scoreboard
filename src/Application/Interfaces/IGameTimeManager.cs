namespace Football.Application.Interfaces;
internal interface IGameTimeManager
{
    int GetQuarter();

    int GetQuarterSecondsRemaining();

    void SetTime();
}
