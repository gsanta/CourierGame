using AI;
using Delivery;
using UnityEngine.AI;
using Route;

namespace Bikers
{
    public class DeliverPackageAction : AbstractRouteAction
    {
        private IDeliveryService deliveryService;
        public DeliverPackageAction(IDeliveryService deliveryService, RouteFacade routeFacade) : base(routeFacade, null)
        {
            this.deliveryService = deliveryService;
        }

        public override bool PrePerform()
        {
            Biker courierAgent = GoapAgent.Parent;

            Package package = courierAgent.GetPackage();
            package.Target.gameObject.SetActive(true);

            var from = agent.GetGoapAgent().Parent.transform;
            var to = package.Target.gameObject.transform;

            StartRoute(from, to);


            return true;
        }

        public override bool PostPerform()
        {
            Biker courierAgent = GoapAgent.Parent;

            Package package = courierAgent.GetPackage();

            deliveryService.DeliverPackage(package, false);

            SubGoal s1 = new SubGoal("isPackageDropped", 1, true);
            GoapAgent.goals.Add(s1, 3);

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

        public override GoapAction<Biker> CloneAndSetup(IGoapAgentProvider<Biker> agent)
        {
            var clone = new DeliverPackageAction(deliveryService, routeFacade);
            clone.agent = agent;
            return clone;
        }
    }
}
