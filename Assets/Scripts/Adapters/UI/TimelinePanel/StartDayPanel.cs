using System;
using TMPro;
using UnityEngine;
using Zenject;

public class StartDayPanel : MonoBehaviour
{

    [SerializeField]
    private TMP_Text dayLabel;

    private Timer timer;

    [Inject]
    public void Construct(Timer timer)
    {
        this.timer = timer;

        timer.DayPassed += HandleDayPassed;
    }

    private void Start()
    {
        dayLabel.SetText("Day " + timer.CurrentDay);
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
        dayLabel.SetText("Day " + timer.CurrentDay);
    }
}
