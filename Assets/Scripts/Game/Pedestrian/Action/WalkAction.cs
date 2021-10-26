using Agents;
using AI;
using Core;
using Route;
using System.Collections.Generic;
using UnityEngine;

namespace Pedestrians
{
    public class WalkAction : AbstractRouteAction<Pedestrian>
    {
        private PedestrianTargetStore pedestrianGoalStore;
        private RouteFacade routeFacade;
        private float hideDuration = 0;
        private GameObject Target;

        public WalkAction(RouteFacade routeFacade, PedestrianTargetStore pedestrianGoalStore, PathCache pathCache) : base(null, pathCache)
        {
            this.routeFacade = routeFacade;
            this.pedestrianGoalStore = pedestrianGoalStore;
        }

        public WalkAction SetTarget(GameObject target)
        {
            Target = target;
            return this;
        }
        public WalkAction SetHideDuration(float duration)
        {
            hideDuration = duration;
            return this;
        }

        public WalkAction SetAfterEffect(AIState worldState)
        {
            afterEffect = worldState;
            return this;
        }

        public override bool PrePerform()
        {
            Pedestrian agent = GoapAgent.Parent;
            agent.navMeshAgent.speed = 2;

            var goal = Target;

            var from = agent.transform;
            var to = goal.transform;

            StartRoute(from, to);

            return true;
        }
        public override bool PostPerform()
        {
            var goals = agent.Parent.GoalProvider.CreateWalkGoal();
            agent.SetGoals(goals, false);
            return true;
        }

        protected override AIState[] GetPreConditions()
        {
            return new AIState[] { };
        }

        protected override AIState[] GetAfterEffects()
        {
            return new AIState[] { afterEffect };
        }

        public override bool PostAbort()
        {
            return true;
        }

        public override GoapAction<Pedestrian> Clone(GoapAgent<Pedestrian> agent = null)
        {
            var action = new WalkAction(routeFacade, pedestrianGoalStore, pathCache);
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
