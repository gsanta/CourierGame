using AI;
using Core;
using Delivery;
using Route;
using System.Collections.Generic;
using UnityEngine;

namespace Bikers
{
    public class DeliverPackageAction : AbstractRouteAction<Biker>
    {
        private DeliveryService deliveryService;
        private RouteFacade routeFacade;
        public DeliverPackageAction(DeliveryService deliveryService, RouteFacade routeFacade) : base(new AIStateName[] { AIStateName.PACKAGE_IS_PICKED }, new AIStateName[] { AIStateName.PACKAGE_IS_DROPPED })
        {
            this.deliveryService = deliveryService;
            this.routeFacade = routeFacade;
        }

        public override bool PrePerform()
        {
            Biker courierAgent = GoapAgent.Parent;

            Package package = courierAgent.GetPackage();
            package.Target.gameObject.SetActive(true);

            var from = agent.Parent.transform;
            var to = package.Target.gameObject.transform;

            StartRoute(from.position, to.position);


            return true;
        }

        public override bool PostPerform()
        {
            Biker courierAgent = GoapAgent.Parent;

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

        public override GoapAction<Biker> Clone(GoapAgent<Biker> agent = null)
        {
            var action = new DeliverPackageAction(deliveryService, routeFacade);
            action.agent = agent;
            return action;
        }

        protected override Queue<Vector3> BuildRoute(Vector3 from, Vector3 to)
        {
            return routeFacade.BuildRoadRoute(from, to);
        }
    }
}
