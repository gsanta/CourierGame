using System;
using UnityEngine;
using Zenject;
public class Timer : MonoBehaviour
{
    [SerializeField]
    private int secondsPerDay = 100;

    private ITimeProvider timeProvider;
    private DateTime time = DateTime.MinValue;
    private int milliSecondAccum = 0;
    private int secondAccum = 0;
    private bool isDayStarted = false;

    public int SecondsPerDay { get => secondsPerDay; }

    public bool IsDayStarted { get => isDayStarted; set => isDayStarted = value; }
    
    [Inject]
    public void Construct(ITimeProvider timeProvider)
    {
        this.timeProvider = timeProvider;
        Elapsed = 0;
    }

    public int Elapsed { get; set; }

    void Update()
    {
        if (isDayStarted)
        {
            Tick();
        }
    }

    public void Pause()
    {
        time = DateTime.MinValue;
    }

    public void Reset()
    {
        milliSecondAccum = 0;
        secondAccum = 0;
    }

    private void Tick()
    {
        DateTime curr = timeProvider.UtcNow();

        if (time != DateTime.MinValue)
        {
            TimeSpan delta = curr.Subtract(time);
            Elapsed += delta.Milliseconds;

            HandleSecondPassed(delta);
            HandleDayPassed();
        }

        time = curr;
    }

    private void HandleSecondPassed(TimeSpan delta)
    {
        milliSecondAccum += delta.Milliseconds;

        if (milliSecondAccum >= 1000)
        {
            SecondPassed?.Invoke(this, EventArgs.Empty);
            milliSecondAccum = milliSecondAccum - 1000;
            secondAccum++;
        }
    }

    private void HandleDayPassed()
    {
        if (secondAccum >= SecondsPerDay)
        {
            DayPassed?.Invoke(this, EventArgs.Empty);
        }
    }

    public float GetDayPercentageAt()
    {
        // Cast is not redundant, otherwise the division is zero if less than one
        return (float) (Elapsed / 1000) / (float) SecondsPerDay;
    }

    public event EventHandler SecondPassed;
    public event EventHandler DayPassed;
}