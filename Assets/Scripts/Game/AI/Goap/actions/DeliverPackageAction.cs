using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AI;

namespace AI
{
    class DeliverPackageAction : GAction<Biker>
    {
        public DeliverPackageAction(IGoapAgentProvider<Biker> agent) : base(agent)
        {
        }

        public override bool PrePerform()
        {
            Biker courierAgent = GoapAgent.Parent;

            Package package = courierAgent.GetPackage();

            target = package.Target.gameObject;

            return true;
        }

        public override bool PostPerform()
        {
            Biker courierAgent = GoapAgent.Parent;

            Package package = courierAgent.GetPackage();

            package.DeliverPackage();

            SubGoal s1 = new SubGoal("isPackageDropped", 1, true);
            GoapAgent.goals.Add(s1, 3);

            return true;
        }

        public override bool IsDestinationReached()
        {
            var navMeshAgent = GoapAgent.NavMeshAgent;
            return navMeshAgent.hasPath && navMeshAgent.remainingDistance < 1f;
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
            throw new NotImplementedException();
        }
    }
}
