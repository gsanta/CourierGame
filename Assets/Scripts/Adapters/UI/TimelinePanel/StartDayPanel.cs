using System;
using UnityEngine;
using Zenject;

public class StartDayPanel : MonoBehaviour
{
    private Timer timer;

    [Inject]
    public void Construct(Timer timer)
    {
        this.timer = timer;

        timer.DayPassed += HandleDayPassed;
    }

    public void OnStart()
    {
        timer.IsDayStarted = true;
        gameObject.SetActive(false);
        timer.Reset();
        //dayMeasurer.OnDayPassed += HandleDayPassed;
    }

    private void HandleDayPassed(object sender, EventArgs e)
    {
        timer.IsDayStarted = false;
        gameObject.SetActive(true);
    }
}
