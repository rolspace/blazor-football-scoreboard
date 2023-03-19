using Football.Application;
using Football.Application.Interfaces;

namespace Football.Application;

public class GameTimeManager : IGameTimeManager
{
    private readonly object _locker = new object();

    private int _gamesFinished = 0;

    private int _secondsRemaining = 50;

    private int _quarter = 4;

    public int GetQuarter() => _quarter;

    public int GetQuarterSecondsRemaining() => _secondsRemaining;

    public void SetTime()
    {
        lock (_locker)
        {
            if (AreAllGamesFinished())
            {
                Interlocked.Exchange(ref _quarter, -1);
                Interlocked.Exchange(ref _secondsRemaining, -1);
                return;
            }

            if (_secondsRemaining > 0)
            {
                Interlocked.Decrement(ref _secondsRemaining);
                return;
            }

            if (_secondsRemaining == 0 && _quarter < Constants.FOURTH_QUARTER)
            {
                Interlocked.Increment(ref _quarter);
                Interlocked.Exchange(ref _secondsRemaining, 900);
                return;
            }

            if (_secondsRemaining == 0 && _quarter == Constants.FOURTH_QUARTER)
            {
                Interlocked.Increment(ref _quarter);
                Interlocked.Exchange(ref _secondsRemaining, 600);
                return;
            }
        }
    }

    public void IncrementGamesFinished(int count)
    {
        lock (_locker)
        {
            Interlocked.Add(ref _gamesFinished, count);
        }
    }

    private bool AreAllGamesFinished()
    {
        return _gamesFinished == Constants.GAMES_PER_WEEK;
    }
}
