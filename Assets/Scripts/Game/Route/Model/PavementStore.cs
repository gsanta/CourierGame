using AI;
using System;
using System.Collections.Generic;

namespace Route
{
    public class PavementStore : IRoadLikeStore
    {
        private List<Waypoint> waypoints = new List<Waypoint>();   

        public List<Waypoint> GetWaypoints() { return waypoints; }

        public void SetWaypoints(List<Waypoint> waypoints)
        {
            this.waypoints = waypoints;
            Initialized?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Initialized;
    }
}
