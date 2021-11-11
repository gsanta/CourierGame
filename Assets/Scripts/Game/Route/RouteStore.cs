using AI;
using Scenes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Route
{
    public class RouteStore : IResetable
    {
        private List<Waypoint> waypoints = new List<Waypoint>();
        private DirectedGraph<Waypoint, object> graph = new DirectedGraph<Waypoint, object>();
        private RouteBuilder routeBuilder;

        public List<Waypoint> GetWaypoints() { return waypoints; }
        public DirectedGraph<Waypoint, object> GetGraph() { return graph; }

        public void SetWaypoints(List<Waypoint> waypoints)
        {
            this.waypoints = waypoints;
            Initialized?.Invoke(this, EventArgs.Empty);

            WaypointGraphBuilder builder = new WaypointGraphBuilder();
            builder.BuildGraph(waypoints, graph);

            routeBuilder = new RouteBuilder(this);
            routeBuilder.Setup();
        }
        public Queue<Vector3> BuildRoute(Vector3 from, Vector3 to)
        {
            return routeBuilder.BuildRoute(from, to);
        }

        public void Reset()
        {
            waypoints = new List<Waypoint>();
        }

        public event EventHandler Initialized;
    }
}
