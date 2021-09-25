using Model;
using AI;
using Delivery;
using Road;
using System.Collections.Generic;
using UnityEngine;

namespace Service
{
    public class RouteAction : GoapAction<Biker>
    {
        private readonly IDeliveryService deliveryService;
        private readonly PackageStore2 packageStore;
        private readonly RoadStore roadStore;

        public RouteAction(IDeliveryService deliveryService, PackageStore2 packageStore, RoadStore roadStore) : base(null)
        {
            this.deliveryService = deliveryService;
            this.packageStore = packageStore;
            this.roadStore = roadStore;
        }

        public void SetAgent(IGoapAgentProvider<Biker> agent)
        {
            this.agent = agent;
        }

        public override bool PrePerform()
        {
            DirectedGraph<Waypoint, object> graph = new DirectedGraph<Waypoint, object>();
            RouteFinder<Waypoint, object> routeFinder = new RouteFinder<Waypoint, object>(graph, new WaypointScorer(), new WaypointScorer());
            WaypointGraphBuilder builder = new WaypointGraphBuilder();
            builder.BuildGraph(this.roadStore.Waypoints, graph);
            NearestItemCalc<Transform, Waypoint> nearestItemCalc = new NearestItemCalc<Transform, Waypoint>(x => x.position, x => x.transform.position);
            
            var nearestToBiker = nearestItemCalc.GetNearest(agent.GetGoapAgent().Parent.transform, roadStore.Waypoints);
            var nearestToPackage = nearestItemCalc.GetNearest(packageStore.Packages[0].transform, roadStore.Waypoints);
            var route = routeFinder.FindRoute(nearestToBiker, nearestToPackage);
            //Biker courierAgent = GoapAgent.Parent;
            //target = courierAgent.GetPackage().gameObject;

            return true;
        }
        public override bool PostPerform()
        {
            //Biker courierAgent = GoapAgent.Parent;

            //Package package = courierAgent.GetPackage();
            //deliveryService.AssignPackage(package);
            return true;
        }

        public override bool IsDestinationReached()
        {
            //var navMeshAgent = GoapAgent.NavMeshAgent;
            //var ret = navMeshAgent.hasPath && navMeshAgent.remainingDistance < 1f;

            //return ret;
            return true;
        }

        protected override WorldState[] GetPreConditions()
        {
            return new WorldState[0];
        }

        protected override WorldState[] GetAfterEffects()
        {
            return new WorldState[] { new WorldState("isTestFinished", 3) };
        }

        public override bool PostAbort()
        {
            return true;
        }
    }
}
