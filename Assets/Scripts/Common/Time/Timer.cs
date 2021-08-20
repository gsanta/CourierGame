using System;
using UnityEngine;
using Zenject;
public class Timer : MonoBehaviour
{
    private ITimeProvider timeProvider;
    private IWorldState worldState;
    private DateTime time = DateTime.MinValue;
    private int secondAccum = 0;

    [Inject]
    public void Construct(ITimeProvider timeProvider, IWorldState worldState)
    {
        this.timeProvider = timeProvider;
        this.worldState = worldState;
        Elapsed = 0;
    }

    public int Elapsed { get; set; }

    void Update()
    {
        if (worldState.IsMeasuring())
        {
            Tick();
        }
    }

    public void Pause()
    {
        time = DateTime.MinValue;
    }

    private void Tick()
    {
        DateTime curr = timeProvider.UtcNow();

        if (time != DateTime.MinValue)
        {
            TimeSpan delta = curr.Subtract(time);
            Elapsed += delta.Milliseconds;

            HandleSecondPassed(delta);
        }

        time = curr;
    }

    private void HandleSecondPassed(TimeSpan delta)
    {
        secondAccum += delta.Milliseconds;

        if (secondAccum >= 1000)
        {
            OnSecondPassed?.Invoke(this, EventArgs.Empty);
            secondAccum = secondAccum - 1000;
        }
    }

    public float GetDayPercentageAt(int elapsedTime)
    {
        // Cast is not redundant, otherwise the division is zero if less than one
        return (float) (elapsedTime / 1000) / (float) worldState.SecondsPerDay();
    }

    public event EventHandler OnSecondPassed;
}