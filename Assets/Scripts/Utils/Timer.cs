﻿using System;

public class Timer
{
    private ITimeProvider timeProvider;
    private DateTime prev = DateTime.MinValue;

    public Timer(ITimeProvider _timeProvider)
    {
        timeProvider = _timeProvider;
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
        return (float) ElapsedSeconds / (float) WorldProperties.Instance().secondsPerDay;
    }

    public int ElapsedSeconds
    {
        get => Accum / 1000;
    }
}