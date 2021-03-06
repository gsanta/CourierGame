
using Agents;
using AI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Scenes
{
    public abstract class AbstractRouteAction<T>: GoapAction<T> where T : IGameObject
    {
        private Queue<Vector3> route;
        protected PathCache pathCache;

        public AbstractRouteAction(AIStateName[] preconditions, AIStateName[] afterEffects, PathCache pathCache = null) : base(preconditions, afterEffects)
        {
            this.pathCache = pathCache;
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

        protected void StartRoute(Vector3 from, Vector3 to)
        {
            route = BuildRoute(from, to);
            UpdateDestination();
        }

        protected abstract Queue<Vector3> BuildRoute(Vector3 from, Vector3 to);

        private void UpdateDestination()
        {
            if (route.Count > 0)
            {
                currentTarget = route.Dequeue();
                NavMeshAgent navMeshAgent = agent.NavMeshAgent;


                //if (pathCache != null && agent.prevAction != null)
                //{
                //    var startTime = DateTime.Now;
                //    var path = pathCache.GetPath((agent.prevAction.currentTarget, currentTarget));
                //    navMeshAgent.SetPath(path);
                //    Debug.Log((DateTime.Now - startTime).Seconds);
                //} else
                //{
                    navMeshAgent.SetDestination(currentTarget);
                //}
            }
            else
            {
                finished = true;
            }
        }
    }
}
