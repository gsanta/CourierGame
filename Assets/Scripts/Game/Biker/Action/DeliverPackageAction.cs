using AI;
using Scenes;
using Delivery;
using Route;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Bikers
{
    public class DeliverPackageAction : AbstractRouteAction<Player>
    {
        private DeliveryService deliveryService;
        private RoadStore roadStore;
        public DeliverPackageAction(DeliveryService deliveryService, RoadStore roadStore) : base(new AIStateName[] { AIStateName.PACKAGE_IS_PICKED }, new AIStateName[] { AIStateName.PACKAGE_IS_DROPPED })
        {
            this.deliveryService = deliveryService;
            this.roadStore = roadStore;
        }

        public override bool PrePerform()
        {
            Player courierAgent = GoapAgent.Parent;

            Package package = courierAgent.GetPackage();
            package.Target.gameObject.SetActive(true);

            var from = agent.Parent.transform;
            var to = package.Target.gameObject.transform;

            StartRoute(from.position, to.position);


            return true;
        }

        public override bool PostPerform()
        {
            Player courierAgent = GoapAgent.Parent;

            Package package = courierAgent.GetPackage();

            deliveryService.DeliverPackage(package, false);

            GoapAgent.worldStates.RemoveState(AIStateName.PACKAGE_IS_PICKED);
            GoapAgent.worldStates.RemoveState(AIStateName.PACKAGE_IS_RESERVED);

            return true;
        }

        public override bool PostAbort()
        {
            return true;
        }

        public override GoapAction<Player> Clone(GoapAgent<Player> agent = null)
        {
            var action = new DeliverPackageAction(deliveryService, roadStore);
            action.agent = agent;
            return action;
        }

        protected override Queue<Vector3> BuildRoute(Vector3 from, Vector3 to)
        {
            return roadStore.BuildRoute(from, to);
        }
    }
}
