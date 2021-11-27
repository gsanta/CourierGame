using AI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Route
{
    public class Road<TNode> : IRoad<TNode> where TNode : class, IMonoBehaviour
    {
        private readonly RouteFinder<TNode, object> routeFinder;
        private List<IMonoBehaviour> nodes = new List<IMonoBehaviour>();

        public Road(DirectedGraph<TNode, object> graph, Scorer<TNode> scorer)
        {
            routeFinder = new RouteFinder<TNode, object>(graph, scorer, scorer);
            nodes = graph.Nodes.ToList().ConvertAll(x => (IMonoBehaviour)x);
        }

        public Queue<Vector3> BuildRoute(Vector3 from, Vector3 to)
        {
            var route = routeFinder.FindRoute(from, to);
            var routePoints = route.Select(wp => wp.GetMonoBehaviour().transform.position).ToList();
            if (routePoints.Count >= 2)
            {
                routePoints.RemoveAt(0);
                routePoints.RemoveAt(routePoints.Count - 1);
            }
            routePoints.Add(to);

            return new Queue<Vector3>(routePoints);
        }

        public List<IMonoBehaviour> GetNodes()
        {
            return nodes;
        }
    }
}
