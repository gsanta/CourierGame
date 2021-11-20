using AI;
using Scenes;
using Delivery;
using Route;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Bikers
{
    public class PickUpPackageAction : AbstractRouteAction<Biker>
    {
        private readonly DeliveryService deliveryService;
        private RoadStore roadStore;

        public PickUpPackageAction(DeliveryService deliveryService, RoadStore roadStore) : base(new AIStateName[] { AIStateName.PACKAGE_IS_RESERVED }, new AIStateName[] { AIStateName.PACKAGE_IS_PICKED })
        {
            this.deliveryService = deliveryService;
            this.roadStore = roadStore;
        }

        public override bool PrePerform()
        {
            Biker courierAgent = GoapAgent.Parent;
            var from = agent.Parent.transform;
            var to = courierAgent.GetPackage().gameObject.transform;

            StartRoute(from.position, to.position);

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
            var action = new PickUpPackageAction(deliveryService, roadStore);
            action.agent = agent;
            return action;
        }

        protected override Queue<Vector3> BuildRoute(Vector3 from, Vector3 to)
        {
            return roadStore.BuildRoute(from, to);
        }
    }
}
