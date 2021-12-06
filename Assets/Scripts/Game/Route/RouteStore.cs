using GameObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Routes
{
    public class RouteStore
    {
        private Dictionary<GameCharacter, List<Vector3>> routes = new Dictionary<GameCharacter, List<Vector3>>();

        public void AddRoute(GameCharacter player, List<Vector3> points)
        {
            routes.Add(player, points);
        }

        public Dictionary<GameCharacter, List<Vector3>> GetRoutes()
        {
            return routes;
        }

        public void Clear()
        {
            routes = new Dictionary<GameCharacter, List<Vector3>>();
        }
    }
}
