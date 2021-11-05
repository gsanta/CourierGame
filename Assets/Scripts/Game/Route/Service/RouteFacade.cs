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

        public Queue<Vector3> BuildRoadRoute(Vector3 from, Vector3 to)
        {
            return roadRouteBuilder.BuildRoute(from, to);
        }

        public Queue<Vector3> BuildPavementRoute(Vector3 from, Vector3 to)
        {
            return pavementRouteBuilder.BuildRoute(from, to);
        }

        public void Setup()
        {
            roadRouteBuilder.Setup();
            pavementRouteBuilder.Setup();
        }
    }
}
