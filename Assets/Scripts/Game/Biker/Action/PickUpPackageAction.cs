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

        public PickUpPackageAction(DeliveryService deliveryService, RouteFacade routeFacade) : base(null, null)
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

            GoapAgent.worldStates.AddState("isPackagePickedUp", 3);
            deliveryService.AssignPackage(package);
            return true;
        }

        protected override AIState[] GetPreConditions()
        {
            return new AIState[] { new AIState("isPackageReserved", 3) };
        }

        protected override AIState[] GetAfterEffects()
        {
            return new AIState[] { new AIState("isPackagePickedUp", 3) };
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
