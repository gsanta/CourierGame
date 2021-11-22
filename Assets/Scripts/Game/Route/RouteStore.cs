using Bikers;
using System.Collections.Generic;
using UnityEngine;

namespace Routes
{
    public class RouteStore
    {
        private Dictionary<Player, List<Vector3>> routes = new Dictionary<Player, List<Vector3>>();

        public void AddRoute(Player player, List<Vector3> points)
        {
            routes.Add(player, points);
        }

        public Dictionary<Player, List<Vector3>> GetRoutes()
        {
            return routes;
        }

        public void Clear()
        {
            routes = new Dictionary<Player, List<Vector3>>();
        }
    }
}
