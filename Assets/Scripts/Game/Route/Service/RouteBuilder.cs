
using AI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Route
{
    public class RouteBuilder
    {
        private readonly RouteStore routeStore;
        //private DirectedGraph<Waypoint, object> graph;
        private RouteFinder<Waypoint, object> routeFinder;
        private NearestItemCalc<Vector3, Waypoint> nearestItemCalc;

        public RouteBuilder(RouteStore routeStore)
        {
            this.routeStore = routeStore;
        }

        public Queue<Vector3> BuildRoute(Vector3 from, Vector3 to)
        {
            var nearestToBiker = nearestItemCalc.GetNearest(from, routeStore.GetWaypoints());
            var nearestToPackage = nearestItemCalc.GetNearest(to, routeStore.GetWaypoints());
            var routeWayPoints = routeFinder.FindRoute(nearestToBiker, nearestToPackage);
            var routePoints = routeWayPoints.Select(wp => wp.Position).ToList();
            if (routePoints.Count >= 2)
            {
                routePoints.RemoveAt(0);
                routePoints.RemoveAt(routePoints.Count - 1);
            }
            routePoints.Add(to);

            return new Queue<Vector3>(routePoints);
        }

        public void Setup()
        {
            //graph = new DirectedGraph<Waypoint, object>();
            routeFinder = new RouteFinder<Waypoint, object>(routeStore.GetGraph(), new WaypointScorer(), new WaypointScorer());
            nearestItemCalc = new NearestItemCalc<Vector3, Waypoint>(x => x, x => x.transform.position);

            //WaypointGraphBuilder builder = new WaypointGraphBuilder();
            //builder.BuildGraph(roadLikeStore.GetWaypoints(), graph);
        }
    }
}
