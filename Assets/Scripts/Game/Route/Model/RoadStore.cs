﻿using AI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Route
{
    public class RoadStore : MonoBehaviour, IRoadLikeStore
    {
        [SerializeField]
        private GameObject waypointContainer;

        private List<Waypoint> waypoints = new List<Waypoint>();

        private void Awake()
        {
            foreach (Transform obj in waypointContainer.transform)
            {
                waypoints.Add(obj.GetComponent<Waypoint>());
            }


            Initialized?.Invoke(this, EventArgs.Empty);
        }

        public List<Waypoint> GetWaypoints() { return waypoints; }

        public event EventHandler Initialized;
    }
}
