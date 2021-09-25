using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class WaypointGraphBuilder
    {

        public void BuildGraph(List<Waypoint> waypoints, DirectedGraph<Waypoint, object> graph)
        {
            waypoints.ForEach(waypoint =>
            {
                if (waypoint.PrevWayPoint != null)
                {
                    AddEdge(graph, waypoint, waypoint.PrevWayPoint);
                }

                if (waypoint.NextWayPoint != null)
                {
                    AddEdge(graph, waypoint, waypoint.NextWayPoint);
                }

                waypoint.Branches.ForEach(branch =>
                {
                    AddEdge(graph, waypoint, branch);
                });
            });
        }

        private void AddEdge(DirectedGraph<Waypoint, object> graph, Waypoint wp1, Waypoint wp2)
        {
            graph.AddEdge(wp1, wp2, Waypoint.Distance(wp1, wp2));
        }
    }
}
