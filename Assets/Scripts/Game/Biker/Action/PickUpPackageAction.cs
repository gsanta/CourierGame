using AI;
using Delivery;
using Route;

namespace Bikers
{
    public class PickUpPackageAction : AbstractRouteAction
    {
        private readonly IDeliveryService deliveryService;

        public PickUpPackageAction(IDeliveryService deliveryService, RouteFacade routeFacade) : base(routeFacade, null)
        {
            this.deliveryService = deliveryService;
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
            deliveryService.AssignPackage(package);
            return true;
        }

        protected override WorldState[] GetPreConditions()
        {
            return new WorldState[] { new WorldState("isPackageReserved", 3) };
        }

        protected override WorldState[] GetAfterEffects()
        {
            return new WorldState[] { new WorldState("isPackagePickedUp", 3) };
        }

        public override bool PostAbort()
        {
            return true;
        }

        public override GoapAction<Biker> CloneAndSetup(GoapAgent<Biker> agent)
        {
            var clone = new PickUpPackageAction(deliveryService, routeFacade);
            clone.agent = agent;
            return clone;
        }
    }
}
