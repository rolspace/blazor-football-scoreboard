namespace Football.Worker;

internal class GameTime
{
    private int _counter = 3600;

    public int GetTime() => _counter;

    public int DecreaseTime() => Interlocked.Decrement(ref _counter);
}
