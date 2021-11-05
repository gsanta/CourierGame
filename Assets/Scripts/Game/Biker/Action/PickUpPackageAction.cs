using AI;
using Core;
using Delivery;
using Route;
using System.Collections.Generic;
using UnityEngine;

namespace Bikers
{
    public class PickUpPackageAction : AbstractRouteAction<Biker>
    {
        private readonly DeliveryService deliveryService;
        private readonly RouteFacade routeFacade;

        public PickUpPackageAction(DeliveryService deliveryService, RouteFacade routeFacade) : base(new AIStateName[] { AIStateName.PACKAGE_IS_RESERVED }, new AIStateName[] { AIStateName.PACKAGE_IS_PICKED })
        {
            this.deliveryService = deliveryService;
            this.routeFacade = routeFacade;
        }

        public override bool PrePerform()
        {
            Biker courierAgent = GoapAgent.Parent;
            var from = agent.Parent.transform;
            var to = courierAgent.GetPackage().gameObject.transform;

            StartRoute(from, to);

            return true;
        }
        public override bool PostPerform()
        {
            Biker courierAgent = GoapAgent.Parent;
            Package package = courierAgent.GetPackage();

            GoapAgent.worldStates.AddStates(GetAfterEffects());
            deliveryService.AssignPackage(package);
            return true;
        }

        public override bool PostAbort()
        {
            return true;
        }

        public override GoapAction<Biker> Clone(GoapAgent<Biker> agent = null)
        {
            var action = new PickUpPackageAction(deliveryService, routeFacade);
            action.agent = agent;
            return action;
        }

        protected override Queue<Vector3> BuildRoute(Transform from, Transform to)
        {
            return routeFacade.BuildRoadRoute(from, to);
        }
    }
}
