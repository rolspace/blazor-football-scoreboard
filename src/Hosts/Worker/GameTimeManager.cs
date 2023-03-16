namespace Football.Worker;

internal class GameTimeManager
{
    private readonly object _locker = new object();

    private int _secondsRemaining = 50;

    private int _quarter = 4;

    private bool _overtime = false;

    public int GetQuarter() => _quarter;

    public int GetQuarterSecondsRemaining() => _secondsRemaining;

    public void PassTime()
    {
        lock (_locker)
        {
            if (_secondsRemaining > 0)
            {
                Interlocked.Decrement(ref _secondsRemaining);
            }
            else if (_secondsRemaining == 0 && _quarter < 4)
            {
                Interlocked.Increment(ref _quarter);
                Interlocked.Exchange(ref _secondsRemaining, 900);
            }
            else if (_secondsRemaining == 0 && _quarter == 4 && _overtime)
            {
                Interlocked.Increment(ref _quarter);
                Interlocked.Exchange(ref _secondsRemaining, 600);
            }
        }
    }

    public bool IsEndOfRegulation => _quarter == 4 && _secondsRemaining == 0;

    public void StartOvertime(bool startOvertime)
    {
        lock (_locker)
        {
            _overtime = startOvertime;
        }
    }

    public void End()
    {
        lock (_locker)
        {
            Interlocked.Exchange(ref _quarter, -1);
            Interlocked.Exchange(ref _secondsRemaining, -1);
        }
    }
}
