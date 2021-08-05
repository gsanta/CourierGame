using System;
using UnityEngine;

public class StartDayPanel : MonoBehaviour
{
    public void OnStart()
    {
        WorldProperties.Instance().isMeasuring = true;
        gameObject.SetActive(false);

        //dayMeasurer.OnDayPassed += HandleDayPassed;
    }

    private void HandleDayPassed(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
    }
}
