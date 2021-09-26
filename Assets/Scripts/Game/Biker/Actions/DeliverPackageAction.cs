using Model;
using AI;
using Delivery;

namespace Bikers
{
    class DeliverPackageAction : GoapAction<Biker>
    {
        private IDeliveryService deliveryService;
        public DeliverPackageAction(IGoapAgentProvider<Biker> agent, IDeliveryService deliveryService) : base(agent)
        {
            this.deliveryService = deliveryService;
        }

        public override bool PrePerform()
        {
            Biker courierAgent = GoapAgent.Parent;

            Package package = courierAgent.GetPackage();
            package.Target.gameObject.SetActive(true);

            target = package.Target.gameObject.transform.position;

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

        public override void Update()
        {
            var navMeshAgent = GoapAgent.NavMeshAgent;
            finished = navMeshAgent.hasPath && navMeshAgent.remainingDistance < 1f;
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
    }
}
