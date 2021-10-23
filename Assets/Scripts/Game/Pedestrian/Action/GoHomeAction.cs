using Agents;
using AI;
using Buildings;
using Core;
using Route;
using System.Collections.Generic;
using UnityEngine;

namespace Pedestrians
{
    public class GoHomeAction : AbstractRouteAction<Pedestrian>
    {
        private BuildingStore buildingStore;
        private RouteFacade routeFacade;
        public WorldState afterEffect;

        public GoHomeAction(RouteFacade routeFacade, PathCache pathCache, BuildingStore buildingStore) : base(null, pathCache)
        {
            this.routeFacade = routeFacade;
            this.buildingStore = buildingStore;
        }

        public override bool PrePerform()
        {
            Pedestrian agent = GoapAgent.Parent;

            var from = agent.transform;
            var to = buildingStore.GetDoor(GoapAgent.Parent.pedestrianInfo.home).transform;

            StartRoute(from, to);

            return true;
        }
        public override bool PostPerform()
        {
            agent.Parent.gameObject.SetActive(false);
            return true;
        }

        protected override WorldState[] GetPreConditions()
        {
            return new WorldState[] { };
        }

        protected override WorldState[] GetAfterEffects()
        {
            return new WorldState[] { new WorldState("atHome", 3) };
        }

        public override bool PostAbort()
        {
            return true;
        }

        public override GoapAction<Pedestrian> Clone(GoapAgent<Pedestrian> agent = null)
        {
            var action = new GoHomeAction(routeFacade, pathCache, buildingStore);
            action.agent = agent;
            return action;
        }

        protected override Queue<Vector3> BuildRoute(Transform from, Transform to)
        {
            return new Queue<Vector3>(new List<Vector3>() { to.position });
            //return routeFacade.BuildPavementRoute(from, to);   
        }
    }
}
