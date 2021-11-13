using Bikers;
using System.Collections.Generic;
using UnityEngine;

namespace Routes
{
    public class RouteStore
    {
        private Dictionary<Biker, List<Vector3>> routes = new Dictionary<Biker, List<Vector3>>();

        public void AddRoute(Biker player, List<Vector3> points)
        {
            routes.Add(player, points);
        }

        public Dictionary<Biker, List<Vector3>> GetRoutes()
        {
            return routes;
        }

        public void Clear()
        {
            routes = new Dictionary<Biker, List<Vector3>>();
        }
    }
}
