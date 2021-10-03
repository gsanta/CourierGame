using System.Collections.Generic;
using UnityEngine;

namespace Route
{
    public class RouteFacade
    {
        private RouteBuilder roadRouteBuilder;
        private RouteBuilder pavementRouteBuilder;

        public RouteFacade(RoadStore roadStore, PavementStore pavementStore)
        {
            roadRouteBuilder = new RouteBuilder(roadStore);
            pavementRouteBuilder = new RouteBuilder(pavementStore);
        }

        public Queue<Vector3> BuildRoadRoute(Transform from, Transform to)
        {
            return roadRouteBuilder.BuildRoute(from, to);
        }

        public Queue<Vector3> BuildPavementRoute(Transform from, Transform to)
        {
            return pavementRouteBuilder.BuildRoute(from, to);
        }
    }
}
