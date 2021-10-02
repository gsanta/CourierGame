
using AI;
using Route;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Bikers
{
    public abstract class AbstractRouteAction: GoapAction<Biker>
    {
        protected readonly RouteFacade routeFacade;
        private Queue<Vector3> route;
        private Vector3 currentTarget;

        public AbstractRouteAction(RouteFacade routeFacade, GoapAgent<Biker> agent) : base(agent)
        {
            this.routeFacade = routeFacade;
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
                currentTarget = route.Dequeue();

                NavMeshAgent navMeshAgent = agent.NavMeshAgent;
                navMeshAgent.SetDestination(currentTarget);
            }
            else
            {
                finished = true;
            }
        }
    }
}
