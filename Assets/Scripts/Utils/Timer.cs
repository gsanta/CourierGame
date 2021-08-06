using System;

public class Timer
{
    private ITimeProvider timeProvider;
    private WorldState worldState;
    private DateTime prev = DateTime.MinValue;

    public Timer(ITimeProvider timeProvider, WorldState worldState)
    {
        this.timeProvider = timeProvider;
        this.worldState = worldState;
        Accum = 0;
    }

    public int Accum { get; set; }

    public void Pause()
    {
        prev = DateTime.MinValue;
    }

    public void Tick()
    {
        DateTime curr = timeProvider.UtcNow();

        if (prev != DateTime.MinValue)
        {
            TimeSpan delta = curr.Subtract(prev);
            Accum += delta.Milliseconds;
        }

        prev = curr;
    }

    public float GetDayPercentage()
    {
        // Cast is not redundant, otherwise the division is zero if less than one
        return (float) ElapsedSeconds / (float) worldState.SecondsPerDay();
    }

    public int ElapsedSeconds
    {
        get => Accum / 1000;
    }
}