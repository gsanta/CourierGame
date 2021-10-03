
using AI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Route
{
    public class RouteBuilder
    {
        private readonly IRoadLikeStore roadLikeStore;
        private DirectedGraph<Waypoint, object> graph;
        private RouteFinder<Waypoint, object> routeFinder;
        private NearestItemCalc<Transform, Waypoint> nearestItemCalc;


        public RouteBuilder(IRoadLikeStore roadLikeStore)
        {
            this.roadLikeStore = roadLikeStore;
        }

        public Queue<Vector3> BuildRoute(Transform from, Transform to)
        {
            if (graph == null)
            {
                throw new InvalidOperationException("RouteFacade is not setup.");
            }

            var nearestToBiker = nearestItemCalc.GetNearest(from, roadLikeStore.GetWaypoints());
            var nearestToPackage = nearestItemCalc.GetNearest(to, roadLikeStore.GetWaypoints());
            var routeWayPoints = routeFinder.FindRoute(nearestToBiker, nearestToPackage);
            var routePoints = routeWayPoints.Select(wp => wp.Position).ToList();
            routePoints.RemoveAt(routePoints.Count - 1);
            routePoints.Add(to.position);

            return new Queue<Vector3>(routePoints);
        }

        public void Setup()
        {
            graph = new DirectedGraph<Waypoint, object>();
            routeFinder = new RouteFinder<Waypoint, object>(graph, new WaypointScorer(), new WaypointScorer());
            nearestItemCalc = new NearestItemCalc<Transform, Waypoint>(x => x.position, x => x.transform.position);

            WaypointGraphBuilder builder = new WaypointGraphBuilder();
            builder.BuildGraph(roadLikeStore.GetWaypoints(), graph);
        }
    }
}
