using AI;
using System.Collections;
using System.Collections.Generic;
using System;
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

        public IWaypointProvider newWaypointProvider;
        public IWaypointProvider recordedWaypointProvider;
        public IWaypointProvider activeWaypointProvider;

        CharacterNavigationController controller;
        public Waypoint currentWaypoint;
        public int direction;

        private void Awake()
        {
            controller = GetComponent<CharacterNavigationController>();

            IWaypointProvider[] waypointProviders = GetComponents<IWaypointProvider>();

            newWaypointProvider = Array.Find(waypointProviders, (IWaypointProvider provider) => provider.IsRecordingProvider() == false);
            recordedWaypointProvider = Array.Find(waypointProviders, (IWaypointProvider provider) => provider.IsRecordingProvider() == true);
            activeWaypointProvider = newWaypointProvider;
        }

        void Start()
        {
            direction = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
            controller.SetDestination(currentWaypoint.GetPosition());
        }

        void Update()
        {
            if (controller.reachedDestination)
            {

                WaypointInfo waypointInfo = activeWaypointProvider.GetNextWaypoint();
                currentWaypoint = waypointInfo.waypoint;
                direction = waypointInfo.direction;

                controller.SetDestination(currentWaypoint.GetPosition());
            }
        }

        event EventHandler<NewWayPointCreatedEventArgs> OnNewWaypointCreated;
    }
}
