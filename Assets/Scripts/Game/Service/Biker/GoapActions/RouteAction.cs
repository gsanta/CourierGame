using Model;
using AI;
using Delivery;

namespace Service
{
    class RouteAction : GoapAction<Biker>
    {
        private readonly IDeliveryService deliveryService;
        private readonly PackageStore2 packageStore;

        public RouteAction(IGoapAgentProvider<Biker> agent, IDeliveryService deliveryService, PackageStore2 packageStore) : base(agent)
        {
            this.deliveryService = deliveryService;
            this.packageStore = packageStore;
        }

        public override bool PrePerform()
        {
            DirectedGraph<Waypoint, object> graph = new DirectedGraph<Waypoint, object>();
            //Biker courierAgent = GoapAgent.Parent;
            //target = courierAgent.GetPackage().gameObject;

            return true;
        }
        public override bool PostPerform()
        {
            //Biker courierAgent = GoapAgent.Parent;

            //Package package = courierAgent.GetPackage();
            //deliveryService.AssignPackage(package);
            return true;
        }

        public override bool IsDestinationReached()
        {
            //var navMeshAgent = GoapAgent.NavMeshAgent;
            //var ret = navMeshAgent.hasPath && navMeshAgent.remainingDistance < 1f;

            //return ret;
            return true;
        }

        protected override WorldState[] GetPreConditions()
        {
            return new WorldState[0];
        }

        protected override WorldState[] GetAfterEffects()
        {
            return new WorldState[] { new WorldState("isTestFinished", 3) };
        }

        public override bool PostAbort()
        {
            return true;
        }
    }
}
