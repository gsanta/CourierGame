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

    public bool IsMeasuring()
    {
        return isMeasuring;
    }

    public void SetMeasuring(bool isMeasuring)
    {
        this.isMeasuring = isMeasuring;
    }
}
