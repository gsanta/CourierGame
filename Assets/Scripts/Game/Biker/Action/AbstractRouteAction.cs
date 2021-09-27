﻿
using AI;
using Route;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Bikers
{
    public abstract class AbstractRouteAction: GoapAction<Biker>
    {
        private readonly RouteFacade routeFacade;
        private Queue<Waypoint> route;
        private Vector3 currentTarget;

        public AbstractRouteAction(RouteFacade routeFacade, IGoapAgentProvider<Biker> agent) : base(agent)
        {
            this.routeFacade = routeFacade;
        }

        public void SetAgent(IGoapAgentProvider<Biker> agent)
        {
            this.agent = agent;
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

        protected void StartRoute(Transform from, Transform to)
        {
            route = routeFacade.BuildRoute(from, to);
            UpdateDestination();
        }

        private void UpdateDestination()
        {
            if (route.Count > 0)
            {
                currentTarget = route.Dequeue().transform.position;

                NavMeshAgent navMeshAgent = agent.GetGoapAgent().NavMeshAgent;
                navMeshAgent.SetDestination(currentTarget);
            }
            else
            {
                finished = true;
            }
        }
    }
}
