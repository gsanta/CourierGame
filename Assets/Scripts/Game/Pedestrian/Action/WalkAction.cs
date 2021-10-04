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
        private PedestrianGoalStore pedestrianGoalStore;
        private RouteFacade routeFacade;
        private float hideDuration = 0;
        private GameObject Target;
        private ActionFeeder actionFeeder;
        private WorldState afterEffect;

        public WalkAction(RouteFacade routeFacade, PedestrianGoalStore pedestrianGoalStore, ActionFeeder actionFeeder) : base(null)
        {
            this.routeFacade = routeFacade;
            this.pedestrianGoalStore = pedestrianGoalStore;
            this.actionFeeder = actionFeeder;
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

            var goal = Target; //pedestrianGoalStore.GetGoals()[Random.Range(0, pedestrianGoalStore.GetGoals().Count - 1)];

            var from = agent.transform;
            var to = goal.transform;

            StartRoute(from, to);

            return true;
        }
        public override bool PostPerform()
        {
            agent.goals.Add(new SubGoal("isDestinationReached", 1, true), 3);
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
            var action = new WalkAction(routeFacade, pedestrianGoalStore, actionFeeder);
            action.agent = agent;
            action.Target = Target;
            action.hideDuration = hideDuration;
            return action;
        }

        protected override Queue<Vector3> BuildRoute(Transform from, Transform to)
        {
            return routeFacade.BuildPavementRoute(from, to);   
        }
    }
}
