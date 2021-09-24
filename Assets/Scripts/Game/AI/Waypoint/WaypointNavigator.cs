using UnityEngine;

namespace AI
{

    public class NewWayPointCreatedEventArgs
    {
        public WaypointInfo waypointInfo;

        NewWayPointCreatedEventArgs(WaypointInfo waypointInfo)
        {
            this.waypointInfo = waypointInfo;
        }
    }

    public class WaypointNavigator : MonoBehaviour
    {

        public IWaypointProvider waypointProvider;

        public CharacterNavigationController controller;
        public Waypoint currentWaypoint;
        public int direction;

        private void Awake()
        {
            controller = GetComponent<CharacterNavigationController>();
            waypointProvider = GetComponent<IWaypointProvider>();
        }

        void Start()
        {
            if (direction == -1)
            {
                direction = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
            }
            controller.SetDestination(currentWaypoint.GetRandomPosition());
        }

        void Update()
        {
            if (controller.reachedDestination)
            {

                WaypointInfo waypointInfo = waypointProvider.GetNextWaypoint();
                direction = waypointInfo.direction;

                controller.SetDestination(waypointInfo.waypoint.GetRandomPosition());
                currentWaypoint = waypointInfo.waypoint;

            }
        }
    }
}
