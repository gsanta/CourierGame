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

        public WalkAction(RouteFacade routeFacade, PedestrianGoalStore pedestrianGoalStore) : base(null)
        {
            this.routeFacade = routeFacade;
            this.pedestrianGoalStore = pedestrianGoalStore;
        }

        public override bool PrePerform()
        {
            Pedestrian agent = GoapAgent.Parent;

            var goal = pedestrianGoalStore.GetGoals()[Random.Range(0, pedestrianGoalStore.GetGoals().Count - 1)];

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
            return new WorldState[] { };
        }

        public override bool PostAbort()
        {
            return true;
        }

        public override GoapAction<Pedestrian> Clone(GoapAgent<Pedestrian> agent = null)
        {
            var action = new WalkAction(routeFacade, pedestrianGoalStore);
            action.agent = agent;
            return action;
        }

        protected override Queue<Vector3> BuildRoute(Transform from, Transform to)
        {
            return routeFacade.BuildPavementRoute(from, to);   
        }
    }
}
