namespace Football.Worker;

internal class GameTime
{
    private readonly object _locker = new object();

    private int _secondsRemaining = 900;

    private int _quarter = 1;

    public int GetQuarterSecondsRemaining() => _secondsRemaining;

    public int GetQuarter() => _quarter;

    public void DecreaseSecondsRemaining()
    {
        lock (_locker)
        {
            if (_secondsRemaining > 0)
            {
                Interlocked.Decrement(ref _secondsRemaining);
            }
            else if (_secondsRemaining == 0 && _quarter < 4)
            {
                Interlocked.Exchange(ref _secondsRemaining, 900);
                Interlocked.Increment(ref _quarter);
            }
            else if (_secondsRemaining == 0 && _quarter == 4)
            {
                Interlocked.Exchange(ref _secondsRemaining, 600);
                Interlocked.Increment(ref _quarter);
            }
        }
    }
}
