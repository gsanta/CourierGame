using AI;
using Delivery;
using Route;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Bikers
{
    public class RouteAction : GoapAction<Biker>
    {
        private readonly IDeliveryService deliveryService;
        private readonly PackageStore2 packageStore;
        private readonly RoadStore roadStore;
        private readonly RouteFacade routeFacade;
        private Queue<Vector3> route;
        private Vector3 currentTarget;

        //private DirectedGraph<Waypoint, object> graph;
        //private RouteFinder<Waypoint, object> routeFinder;
        //private NearestItemCalc<Transform, Waypoint> nearestItemCalc;
        private int packageIndex = 0;

        public RouteAction(IDeliveryService deliveryService, PackageStore2 packageStore, RoadStore roadStore, RouteFacade routeFacade) : base(null)
        {
            this.deliveryService = deliveryService;
            this.packageStore = packageStore;
            this.roadStore = roadStore;
            this.routeFacade = routeFacade;
        }

        public override bool PrePerform()
        {
            //graph = new DirectedGraph<Waypoint, object>();
            //routeFinder = new RouteFinder<Waypoint, object>(graph, new WaypointScorer(), new WaypointScorer());
            //WaypointGraphBuilder builder = new WaypointGraphBuilder();
            //builder.BuildGraph(this.roadStore.Waypoints, graph);
            //nearestItemCalc = new NearestItemCalc<Transform, Waypoint>(x => x.position, x => x.transform.position);

            SetupDestination();
            //Biker courierAgent = GoapAgent.Parent;

            UpdateDestination();

            return true;
        }
        public override bool PostPerform()
        {
            //Biker courierAgent = GoapAgent.Parent;

            //Package package = courierAgent.GetPackage();
            //deliveryService.AssignPackage(package);
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

        public override void Update()
        {
            if (IsDestinationReached())
            {
                UpdateDestination();
            }
        }

        private bool IsDestinationReached()
        {
            var navMeshAgent = GoapAgent.NavMeshAgent;
            return navMeshAgent.hasPath && navMeshAgent.remainingDistance < 1f;
        }

        private void UpdateDestination()
        {
            if (route.Count > 0)
            {
                currentTarget = route.Dequeue();

                NavMeshAgent navMeshAgent = agent.NavMeshAgent;
                navMeshAgent.SetDestination(currentTarget);
            } else if (packageIndex < 3)
            {
                SetupDestination();
                UpdateDestination();
            }
            else
            {
                finished = true;
            }
        }

        private void SetupDestination()
        {
            var from = agent.Parent.transform;
            var to = packageStore.Packages[packageIndex].transform;
            route = routeFacade.BuildRoadRoute(from, to);
            packageIndex++;
        }

        public override GoapAction<Biker> Clone(GoapAgent<Biker> agent)
        {
            var clone = new RouteAction(deliveryService, packageStore, roadStore, routeFacade);
            clone.agent = agent;
            return clone;
        }
    }
}
