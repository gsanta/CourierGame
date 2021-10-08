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
        private WorldState afterEffect;

        public WalkAction(RouteFacade routeFacade, PedestrianTargetStore pedestrianGoalStore) : base(null)
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

        public WalkAction SetAfterEffect(WorldState worldState)
        {
            afterEffect = worldState;
            return this;
        }

        public override bool PrePerform()
        {
            Pedestrian agent = GoapAgent.Parent;

            var goal = Target;

            var from = agent.transform;
            var to = goal.transform;

            StartRoute(from, to);

            return true;
        }
        public override bool PostPerform()
        {
            return true;
        }

        protected override WorldState[] GetPreConditions()
        {
            return new WorldState[] { };
        }

        protected override WorldState[] GetAfterEffects()
        {
            return new WorldState[] { afterEffect };
        }

        public override bool PostAbort()
        {
            return true;
        }

        public override GoapAction<Pedestrian> Clone(GoapAgent<Pedestrian> agent = null)
        {
            var action = new WalkAction(routeFacade, pedestrianGoalStore);
            action.agent = agent;
            action.Target = Target;
            action.hideDuration = hideDuration;
            action.afterEffect = afterEffect;
            return action;
        }

        protected override Queue<Vector3> BuildRoute(Transform from, Transform to)
        {
            return routeFacade.BuildPavementRoute(from, to);   
        }
    }
}
