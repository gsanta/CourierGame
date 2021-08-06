using System;
using UnityEngine;

public class StartDayPanel : MonoBehaviour
{
    private IWorldState worldState;

    public void SetDependencies(IWorldState worldState)
    {
        this.worldState = worldState;
    }

    public void OnStart()
    {
        worldState.SetMeasuring(true);
        gameObject.SetActive(false);

        //dayMeasurer.OnDayPassed += HandleDayPassed;
    }

    private void HandleDayPassed(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
    }
}
