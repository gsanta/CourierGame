using Model;
using AI;

namespace Service
{
    class PickUpPackageAction : GoapAction<Biker>
    {
        private readonly IDeliveryService deliveryService;

        public PickUpPackageAction(IGoapAgentProvider<Biker> agent, IDeliveryService deliveryService) : base(agent)
        {
            this.deliveryService = deliveryService;
        }

        public override bool PrePerform()
        {
            Biker courierAgent = GoapAgent.Parent;
            target = courierAgent.GetPackage().gameObject.transform.position;

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
            var navMeshAgent = GoapAgent.NavMeshAgent;
            finished = navMeshAgent.hasPath && navMeshAgent.remainingDistance < 1f;
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
