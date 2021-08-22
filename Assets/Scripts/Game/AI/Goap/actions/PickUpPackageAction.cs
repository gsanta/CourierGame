using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    class PickUpPackageAction : GAction
    {
        public PickUpPackageAction(GAgent agent) : base(agent)
        {
        }

        public override bool PrePerform()
        {
            CourierAgent courierAgent = (CourierAgent)agent;
            target = courierAgent.GetPackage().gameObject;

            return true;
        }
        public override bool PostPerform()
        {
            CourierAgent courierAgent = (CourierAgent)agent;

            Package package = courierAgent.GetPackage();
            package.PickupBy(courierAgent);
            return true;
        }

        public override bool IsDestinationReached()
        {
            var navMeshAgent = agent.GetComponent<NavMeshAgent>();
            return navMeshAgent.hasPath && navMeshAgent.remainingDistance < 1f;
        }

        protected override WorldState[] GetPreConditions()
        {
            return new WorldState[] { new WorldState("isPackageReserved", 3) };
        }

        protected override WorldState[] GetAfterEffects()
        {
            return new WorldState[] { new WorldState("isPackagePickedUp", 3) };
        }
    }
}
