﻿using AI;
using Core;
using System;
using System.Collections.Generic;

namespace Route
{
    public class RoadStore : IRoadLikeStore, IClearable
    {
        private List<Waypoint> waypoints = new List<Waypoint>();    

        public List<Waypoint> GetWaypoints() { return waypoints; }

        public void SetWaypoints(List<Waypoint> waypoints)
        {
            this.waypoints = waypoints;
            Initialized?.Invoke(this, EventArgs.Empty);
        }

        public void Clear()
        {
            waypoints = new List<Waypoint>();
        }

        public event EventHandler Initialized;
    }
}
