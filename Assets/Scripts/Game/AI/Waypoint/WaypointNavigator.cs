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

        public IWaypointProvider waypointProvider;

        CharacterNavigationController controller;
        public Waypoint currentWaypoint;
        public int direction;

        private void Awake()
        {
            controller = GetComponent<CharacterNavigationController>();
            waypointProvider = GetComponent<IWaypointProvider>();
        }

        void Start()
        {
            Debug.Log("layer: " + gameObject.layer);
            direction = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
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
