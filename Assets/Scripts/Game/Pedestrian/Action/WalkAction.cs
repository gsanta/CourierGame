using Agents;
using AI;
using Core;
using Route;
using System.Collections.Generic;
using UnityEngine;

namespace Pedestrians
{
    public class WalkAction<T> : AbstractRouteAction<T> where T : IGameObject
    {
        private WalkTargetStore walkTargetStore;
        private RouteFacade routeFacade;
        private float hideDuration = 0;

        public WalkAction(AIStateName[] preconditions, AIStateName[] afterEffects, RouteFacade routeFacade, WalkTargetStore walkTargetStore, PathCache pathCache) : base(preconditions, afterEffects, pathCache)
        {
            this.routeFacade = routeFacade;
            this.walkTargetStore = walkTargetStore;
        }

        public WalkAction<T> SetHideDuration(float duration)
        {
            hideDuration = duration;
            return this;
        }

        public override bool PrePerform()
        {
            T agent = GoapAgent.Parent;
            agent.GetNavMeshAgent().speed = 2;
            Goal goal = GoapAgent.GetCurrentGoal();

            var from = agent.GetGameObject().transform;
            var to = (Vector3) goal.target;

            StartRoute(from.position, to);

            return true;
        }
        public override bool PostPerform()
        {
            var goals = agent.Parent.GetGoalProvider().CreateGoal();
            agent.SetGoals(goals, false);
            return true;
        }

        public override bool PostAbort()
        {
            return true;
        }

        public override GoapAction<T> Clone(GoapAgent<T> agent = null)
        {
            var action = new WalkAction<T>(GetPreConditions(), GetAfterEffects(), routeFacade, walkTargetStore, pathCache);
            action.agent = agent;
            action.hideDuration = hideDuration;
            return action;
        }

        protected override Queue<Vector3> BuildRoute(Vector3 from, Vector3 to)
        {
            return new Queue<Vector3>(new List<Vector3>() { to });
        }
    }
}
