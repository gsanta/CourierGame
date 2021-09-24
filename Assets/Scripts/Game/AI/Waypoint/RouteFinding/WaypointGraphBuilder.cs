using System.Collections.Generic;

namespace AI
{
    public class WaypointGraphBuilder
    {

        public void BuildGraph(List<IWaypoint> waypoints, DirectedGraph<IWaypoint, object> graph)
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

        private void AddEdge(DirectedGraph<IWaypoint, object> graph, IWaypoint wp1, IWaypoint wp2)
        {
            graph.AddEdge(wp1, wp2, Waypoint.Distance(wp1, wp2));
        }
    }
}
