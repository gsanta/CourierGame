using UnityEngine;

public class WorldProperties : MonoBehaviour
{
    public int secondsPerDay = 100;
    public bool isMeasuring = false;

    private static WorldProperties instance;
    public static WorldProperties Instance()
    {
        return instance;
    }

    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
    }
}
