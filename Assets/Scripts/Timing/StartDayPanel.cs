using System;
using UnityEngine;
using Zenject;

public class StartDayPanel : MonoBehaviour
{
    private IWorldState worldState;

    [Inject]
    public void Construct(IWorldState worldState)
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
