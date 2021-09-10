using UnityEngine;

namespace Service.AI
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
