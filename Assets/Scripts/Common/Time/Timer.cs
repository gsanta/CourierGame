using System;
using UnityEngine;
using Zenject;
public class Timer : MonoBehaviour
{
    [SerializeField]
    private int secondsPerDay = 100;

    private ITimeProvider timeProvider;
    private DateTime time = DateTime.MinValue;
    private long milliSecondAccum = 0;
    private int secondAccum = 0;
    private bool isDayStarted = false;
    private int currentDay = 1;

    private int daySecondsCounter = 0;

    public int SecondsPerDay { get => secondsPerDay; }

    public bool IsDayStarted
    {
        get => isDayStarted;
        set
        {
            if (value)
            {
                isDayStarted = true;
                HandleDayStarted();
            }
            else
            {
                isDayStarted = false;
            }
        }
    }
    
    [Inject]
    public void Construct(ITimeProvider timeProvider)
    {
        this.timeProvider = timeProvider;
        Elapsed = 0;
    }

    public int CurrentDay { get => currentDay; }

    public int Elapsed { get; set; }

    void Update()
    {
        Tick();
    }
    
    private void Tick()
    {
        DateTime curr = timeProvider.UtcNow();

        if (time != DateTime.MinValue)
        {
            TimeSpan delta = curr.Subtract(time);
            Elapsed += delta.Milliseconds;

            HandleSecondPassed(delta);
            if (isDayStarted)
            {
                HandleDayPassed();
            }
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

            if (isDayStarted)
            {
                daySecondsCounter++;
            }
        }
    }

    private void HandleDayPassed()
    {
        if (daySecondsCounter >= SecondsPerDay)
        {
            currentDay++;
            IsDayStarted = false;
            DayPassed?.Invoke(this, EventArgs.Empty);
        }
    }

    private void HandleDayStarted()
    {
        daySecondsCounter = 0;
        DayStarted?.Invoke(this, EventArgs.Empty);
    }

    public float GetDayPercentage()
    {
        // Cast is not redundant, otherwise the division is zero if less than one
        return ((float) Elapsed / 1000f) / (float) SecondsPerDay;
    }

    public event EventHandler SecondPassed;
    public event EventHandler DayPassed;
    public event EventHandler DayStarted;
}