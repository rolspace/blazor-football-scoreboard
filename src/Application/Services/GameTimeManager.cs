using Football.Application;
using Football.Application.Interfaces;

namespace Football.Application.Services;

public class GameTimeManager : IGameTimeManager
{
    private readonly object _locker = new object();

    private int _gamesOver;

    private int _quarterSecondsRemaining;

    private int _quarter;

    public GameTimeManager()
        : this(Constants.FIRST_QUARTER, Constants.SECONDS_IN_QUARTER) { }

    public GameTimeManager(int quarter, int quarterSecondsRemaining)
    {
        _quarter = quarter;
        _quarterSecondsRemaining = quarterSecondsRemaining;
    }

    public int GetQuarter() => _quarter;

    public int GetQuarterSecondsRemaining() => _quarterSecondsRemaining;

    public void SetTime()
    {
        lock (_locker)
        {
            if (_gamesOver == Constants.GAMES_PER_WEEK)
            {
                Interlocked.Exchange(ref _quarter, -1);
                Interlocked.Exchange(ref _quarterSecondsRemaining, -1);
                return;
            }

            if (_quarterSecondsRemaining > 0)
            {
                Interlocked.Decrement(ref _quarterSecondsRemaining);
                return;
            }

            if (_quarterSecondsRemaining == 0 && _quarter < Constants.FOURTH_QUARTER)
            {
                Interlocked.Increment(ref _quarter);
                Interlocked.Exchange(ref _quarterSecondsRemaining, 900);
                return;
            }

            if (_quarterSecondsRemaining == 0 && _quarter == Constants.FOURTH_QUARTER)
            {
                Interlocked.Increment(ref _quarter);
                Interlocked.Exchange(ref _quarterSecondsRemaining, 600);
                return;
            }
        }
    }

    public void IncrementGamesOver(int count)
    {
        lock (_locker)
        {
            Interlocked.Add(ref _gamesOver, count);
        }
    }
}