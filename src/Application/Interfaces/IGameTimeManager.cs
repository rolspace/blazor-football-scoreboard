namespace Football.Application.Interfaces;
internal interface IGameTimeManager
{
    bool IsEndOfRegulation { get; }

    int GetQuarter();

    int GetQuarterSecondsRemaining();

    void PassTime();

    void StartOvertime(bool startOvertime);

    void End();
}
