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

        public GoHomeAction(RouteFacade routeFacade, PathCache pathCache, BuildingStore buildingStore) : base(new AIStateName[] { }, new AIStateName[] { AIStateName.AT_HOME })
        {
            this.routeFacade = routeFacade;
            this.buildingStore = buildingStore;
            duration = 0;
        }

        public override bool PrePerform()
        {
            Pedestrian agent = GoapAgent.Parent;
            agent.navMeshAgent.speed = 4;

            var from = agent.transform;
            var to = buildingStore.GetDoor(GoapAgent.Parent.pedestrianInfo.home).transform;

            StartRoute(from.position, to.position);

            return true;
        }
        public override bool PostPerform()
        {
            agent.Parent.gameObject.SetActive(false);
            return true;
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

        protected override Queue<Vector3> BuildRoute(Vector3 from, Vector3 to)
        {
            return new Queue<Vector3>(new List<Vector3>() { to });
        }
    }
}
