using AI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Route
{
    public class PavementStore : MonoBehaviour, IRoadLikeStore
    {
        [SerializeField]
        private GameObject WaypointContainer;

        private List<Waypoint> waypoints = new List<Waypoint>();

        private void Awake()
        {
            foreach (Transform obj in WaypointContainer.transform)
            {
                waypoints.Add(obj.GetComponent<Waypoint>());
            }


            Initialized?.Invoke(this, EventArgs.Empty);
        }

        public List<Waypoint> GetWaypoints() { return waypoints; }

        public event EventHandler Initialized;
    }
}
