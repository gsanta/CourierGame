using System;
using UnityEngine;

public class StartDayPanel : MonoBehaviour
{
    [HideInInspector] public DayMeasurer dayMeasurer;
    public void OnStart()
    {
        dayMeasurer.StartMeasure();
        gameObject.SetActive(false);

        dayMeasurer.OnDayPassed += HandleDayPassed;
    }

    private void HandleDayPassed(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
    }
}
