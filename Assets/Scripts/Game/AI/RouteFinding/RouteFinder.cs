
using System;
using System.Collections.Generic;

namespace AI
{
    public class RouteFinder<TNode, TEdge>
    {
        private readonly DirectedGraph<TNode, TEdge> graph;
        private readonly Scorer<TNode> nextNodeScorer;
        private readonly Scorer<TNode> targetScorer;

        public RouteFinder(DirectedGraph<TNode, TEdge> graph, Scorer<TNode> nextNodeScorer, Scorer<TNode> targetScorer)
        {
            this.graph = graph;
            this.nextNodeScorer = nextNodeScorer;
            this.targetScorer = targetScorer;
        }

        public List<TNode> FindRoute(TNode from, TNode to)
        {
            PriorityQueue<RouteNode<TNode>> openSet = new PriorityQueue<RouteNode<TNode>>();
            Dictionary<TNode, RouteNode<TNode>> routeNodeMap = new Dictionary<TNode, RouteNode<TNode>>();

            RouteNode<TNode> startRouteNode = new RouteNode<TNode>(from, default(TNode), 0d, targetScorer.computeCost(from, to));
            
            openSet.Enqueue(startRouteNode);
            routeNodeMap[from] = startRouteNode;

            var lastRouteNode = BuildRoute(to, openSet, routeNodeMap);
            return CreateList(lastRouteNode, routeNodeMap);
        }

        private RouteNode<TNode> BuildRoute(TNode to, PriorityQueue<RouteNode<TNode>> openSet, Dictionary<TNode, RouteNode<TNode>> routeNodeMap)
        {
            while (openSet.Count > 0)
            {
                RouteNode<TNode> nextRouteNode = openSet.Dequeue();
                if (nextRouteNode.Current.Equals(to))
                {
                    return nextRouteNode;
                }

                UpdateNeighbours(nextRouteNode, to, openSet, routeNodeMap);
            }

            throw new InvalidOperationException("No route found.");
        }

        private void UpdateNeighbours(RouteNode<TNode> currentRouteNode, TNode to, PriorityQueue<RouteNode<TNode>> openSet, Dictionary<TNode, RouteNode<TNode>> routeNodeMap)
        {
            foreach (var item in graph.OutgoingNodes(currentRouteNode.Current))
            {
                RouteNode<TNode> nextRouteNode = routeNodeMap.ContainsKey(item) ? routeNodeMap[item] : new RouteNode<TNode>(item);
                routeNodeMap[item] = nextRouteNode;

                double newScore = currentRouteNode.RouteScore + nextNodeScorer.computeCost(currentRouteNode.Current, item);

                if (newScore < nextRouteNode.RouteScore)
                {
                    nextRouteNode.Previous = currentRouteNode.Current;
                    nextRouteNode.RouteScore = newScore;
                    nextRouteNode.EstimatedScore = newScore + targetScorer.computeCost(item, to);
                    openSet.Enqueue(nextRouteNode);
                }
            }
        }

        private List<TNode> CreateList(RouteNode<TNode> lastRouteNode, Dictionary<TNode, RouteNode<TNode>> routeNodeMap)
        {
            List<TNode> route = new List<TNode>();
            RouteNode<TNode> currentRouteNode = lastRouteNode;
            do
            {
                route.Insert(0, currentRouteNode.Current);
                currentRouteNode = routeNodeMap[currentRouteNode.Previous];
            } 
            while (currentRouteNode != null);
            
            return route;
        }
    }
}
