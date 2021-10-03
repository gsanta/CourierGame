
using AI;
using Route;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    public abstract class AbstractRouteAction<T>: GoapAction<T>
    {
        private Queue<Vector3> route;
        private Vector3 currentTarget;

        public AbstractRouteAction(GoapAgent<T> agent) : base(agent)
        {
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
            route = BuildRoute(from, to);
            UpdateDestination();
        }

        protected abstract Queue<Vector3> BuildRoute(Transform from, Transform to);

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
