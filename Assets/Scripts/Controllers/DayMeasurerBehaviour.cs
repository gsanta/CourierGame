using System;
using UnityEngine;

public class DayMeasurerBehaviour : MonoBehaviour
{
    [SerializeField] private float daySeconds = 100f;
    [HideInInspector] public DayMeasurer dayMeasurer;

    void Start()
    {
        dayMeasurer.DaySeconds = daySeconds;
    }

    void Update()
    {
        if (dayMeasurer.IsMeasuring && !dayMeasurer.IsDayPassed)
        {
            dayMeasurer.UpdateTime();
        }
    }
}

public class DayMeasurer
{
    private ITimeProvider timeProvider;
    private DateTime startTime;
    private DateTime currTime;
    private bool isMeasuring = false;
    private bool isDayPassed = false;

    public DayMeasurer(ITimeProvider _timeProvider)
    {
        timeProvider = _timeProvider;
    }

    public float DaySeconds { get; set; }
    public bool IsMeasuring { get => isMeasuring; }
    public bool IsDayPassed { get => isDayPassed; }

    public void StartMeasure()
    {
        startTime = timeProvider.UtcNow();
        isMeasuring = true;
        isDayPassed = false;
    }

    public void UpdateTime()
    {
        currTime = timeProvider.UtcNow();

        if (GetDayPercentage() >= 1)
        {
            currTime = startTime.AddSeconds(DaySeconds);
            isDayPassed = true;

            OnDayPassed?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ResetMeasure()
    {
        isMeasuring = false;
        isDayPassed = false;
    }


    public float GetDayPercentage()
    {
        return GetTimeInSec() / DaySeconds;
    }

    public int GetTimeInSec()
    {
        return (currTime - startTime).Seconds;
    }

    public event EventHandler OnDayPassed;

}