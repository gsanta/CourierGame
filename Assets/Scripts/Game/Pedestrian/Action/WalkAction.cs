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
        private GameObject Target;

        public WalkAction(RouteFacade routeFacade, WalkTargetStore walkTargetStore, PathCache pathCache) : base(null, pathCache)
        {
            this.routeFacade = routeFacade;
            this.walkTargetStore = walkTargetStore;
        }

        public WalkAction<T> SetTarget(GameObject target)
        {
            Target = target;
            return this;
        }

        public WalkAction<T> SetHideDuration(float duration)
        {
            hideDuration = duration;
            return this;
        }

        public WalkAction<T> SetAfterEffect(AIState worldState)
        {
            afterEffect = worldState;
            return this;
        }

        public override bool PrePerform()
        {
            T agent = GoapAgent.Parent;
            agent.GetNavMeshAgent().speed = 2;

            var goal = Target;

            var from = agent.GetGameObject().transform;
            var to = goal.transform;

            StartRoute(from, to);

            return true;
        }
        public override bool PostPerform()
        {
            var goals = agent.Parent.GetGoalProvider().CreateGoal();
            agent.SetGoals(goals, false);
            return true;
        }

        protected override AIState[] GetPreConditions()
        {
            return new AIState[] { };
        }

        protected override AIState[] GetAfterEffects()
        {
            return afterEffect == null ? new AIState[] { } : new AIState[] { afterEffect };
        }

        public override bool PostAbort()
        {
            return true;
        }

        public override GoapAction<T> Clone(GoapAgent<T> agent = null)
        {
            var action = new WalkAction<T>(routeFacade, walkTargetStore, pathCache);
            action.agent = agent;
            action.Target = Target;
            action.hideDuration = hideDuration;
            action.afterEffect = afterEffect;
            return action;
        }

        protected override Queue<Vector3> BuildRoute(Transform from, Transform to)
        {
            return new Queue<Vector3>(new List<Vector3>() { to.position });
            //return routeFacade.BuildPavementRoute(from, to);   
        }
    }
}
