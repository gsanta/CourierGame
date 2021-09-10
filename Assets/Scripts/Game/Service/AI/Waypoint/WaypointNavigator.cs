using System;
using UnityEngine;

namespace Service.AI
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
            controller.SetDestination(currentWaypoint.GetPosition());
        }

        void Update()
        {
            if (controller.reachedDestination)
            {

                WaypointInfo waypointInfo = waypointProvider.GetNextWaypoint();
                currentWaypoint = waypointInfo.waypoint;
                direction = waypointInfo.direction;

                controller.SetDestination(currentWaypoint.GetPosition());
            }
        }

        event EventHandler<NewWayPointCreatedEventArgs> OnNewWaypointCreated;
    }
}
