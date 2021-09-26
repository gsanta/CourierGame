﻿using Model;
using AI;
using Delivery;
using Road;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Service
{
    public class RouteAction : GoapAction<Biker>
    {
        private readonly IDeliveryService deliveryService;
        private readonly PackageStore2 packageStore;
        private readonly RoadStore roadStore;
        private Queue<Waypoint> route;
        private Vector3 currentTarget;

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
            var routeNodes = routeFinder.FindRoute(nearestToBiker, nearestToPackage);
            route = new Queue<Waypoint>(routeNodes);
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
                currentTarget = route.Dequeue().transform.position;

                NavMeshAgent navMeshAgent = agent.GetGoapAgent().NavMeshAgent;
                navMeshAgent.SetDestination(currentTarget);
            } else
            {
                finished = true;
            }
        }
    }
}
