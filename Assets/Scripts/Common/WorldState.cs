using UnityEngine;

public class WorldState : MonoBehaviour, IWorldState
{
    public int secondsPerDay;
    public bool isMeasuring;

    void Start()
    {
        isMeasuring = false;
        secondsPerDay = 100;
    }

    public int SecondsPerDay()
    {
        return secondsPerDay;
    }

    public bool IsDayStarted()
    {
        return isMeasuring;
    }

    public void StartDay()
    {
        this.isMeasuring = true;
    }

    public void EndDay()
    {
        this.isMeasuring = false;
    }
}
