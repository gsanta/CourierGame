using UnityEngine;

public class WorldState : MonoBehaviour, IWorldState
{
    public int secondsPerDay = 100;
    public bool isMeasuring = false;

    public int SecondsPerDay()
    {
        return secondsPerDay;
    }

    public bool IsMeasuring()
    {
        return isMeasuring;
    }
}
