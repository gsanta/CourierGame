using AI;
using Delivery;
using Route;

namespace Bikers
{
    class PickUpPackageAction : AbstractRouteAction
    {
        private readonly IDeliveryService deliveryService;

        public PickUpPackageAction(IGoapAgentProvider<Biker> agent, IDeliveryService deliveryService, RouteFacade routeFacade) : base(routeFacade, agent)
        {
            this.deliveryService = deliveryService;
        }

        public override bool PrePerform()
        {
            Biker courierAgent = GoapAgent.Parent;
            var from = agent.GetGoapAgent().Parent.transform;
            var to = courierAgent.GetPackage().gameObject.transform;

            StartRoute(from, to);
            //NavMeshAgent navMeshAgent = agent.GetGoapAgent().NavMeshAgent;
            //navMeshAgent.SetDestination(target);

            return true;
        }
        public override bool PostPerform()
        {
            Biker courierAgent = GoapAgent.Parent;

            Package package = courierAgent.GetPackage();
            deliveryService.AssignPackage(package);
            return true;
        }

        public override void Update()
        {
            base.Update();
            //var navMeshAgent = GoapAgent.NavMeshAgent;
            //finished = navMeshAgent.hasPath && navMeshAgent.remainingDistance < 1f;
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
    }
}
