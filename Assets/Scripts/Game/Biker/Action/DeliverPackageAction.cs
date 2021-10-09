﻿using AI;
using Delivery;
using UnityEngine.AI;
using Route;
using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Bikers
{
    public class DeliverPackageAction : AbstractRouteAction<Biker>
    {
        private IDeliveryService deliveryService;
        private RouteFacade routeFacade;
        public DeliverPackageAction(IDeliveryService deliveryService, RouteFacade routeFacade) : base(null, null)
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

            StartRoute(from, to);


            return true;
        }

        public override bool PostPerform()
        {
            Biker courierAgent = GoapAgent.Parent;

            Package package = courierAgent.GetPackage();

            deliveryService.DeliverPackage(package, false);

            GoapAgent.worldStates.RemoveState("isPackagePickedUp");
            GoapAgent.worldStates.RemoveState("isPackageReserved");

            return true;
        }

        protected override WorldState[] GetPreConditions()
        {
            return new WorldState[] { new WorldState("isPackagePickedUp", 3) };
        }

        protected override WorldState[] GetAfterEffects()
        {
            return new WorldState[] { new WorldState("isPackageDropped", 3) };
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

        protected override Queue<Vector3> BuildRoute(Transform from, Transform to)
        {
            return routeFacade.BuildRoadRoute(from, to);
        }
    }
}
