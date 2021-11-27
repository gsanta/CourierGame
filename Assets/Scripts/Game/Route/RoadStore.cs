using AI;
using Scenes;
using System.Collections.Generic;

namespace Route
{
    public class RoadStore : IResetable
    {
        private Dictionary<string, IRoad<IMonoBehaviour>> roads = new Dictionary<string, IRoad<IMonoBehaviour>>();

        public void AddRoad(string name, IRoad<IMonoBehaviour> road)
        {
            roads.Add(name, road);
        }

        public IRoad<IMonoBehaviour> GetRoad(string name)
        {
            return roads[name];
        }

        //public List<Waypoint> GetWaypoints() { return waypoints; }
        //public DirectedGraph<Waypoint, object> GetGraph() { return graph; }

        //public void SetWaypoints(List<Waypoint> waypoints)
        //{
        //    this.waypoints = waypoints;

        //    WaypointGraphBuilder builder = new WaypointGraphBuilder();
        //    builder.BuildGraph(waypoints, graph);

        //    routeBuilder = new RouteBuilder(this);
        //    routeBuilder.Setup();
        //}
        //public Queue<Vector3> BuildRoute(Vector3 from, Vector3 to)
        //{
        //    return routeBuilder.BuildRoute(from, to);
        //}

        public void Reset()
        {
            //waypoints = new List<Waypoint>();
        }
    }
}
