using System;
using UnityEngine;

public class StartDayPanel : MonoBehaviour
{
    private WorldState worldState;

    public void SetDependencies(WorldState worldState)
    {
        this.worldState = worldState;
    }

    public void OnStart()
    {
        worldState.isMeasuring = true;
        gameObject.SetActive(false);

        //dayMeasurer.OnDayPassed += HandleDayPassed;
    }

    private void HandleDayPassed(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
    }
}
