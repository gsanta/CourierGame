using UnityEngine;

namespace AI
{
    public class WaypointRecorder : MonoBehaviour
    {
        private WaypointNavigator waypointNavigator;

        void Start()
        {
            waypointNavigator = GetComponent<WaypointNavigator>();
        }

    }
}
