using AI;
using System.Collections.Generic;
using Delivery;

namespace Bikers
{
    public class ReservePackageAction : GoapAction<Biker>
    {

        private PackageStore packageStore;
        private readonly DeliveryService deliveryService;

        public ReservePackageAction(PackageStore packageStore, DeliveryService deliveryService) : base(null)
        {
            this.packageStore = packageStore;
            this.deliveryService = deliveryService;
        }

        public override void Update()
        {
            finished = true;
        }

        public override bool PostAbort()
        {
            return true;
        }

        public override bool PostPerform()
        {
            List<Package> packages = packageStore.GetAllPickable();

            if (packages.Count == 0)
            {
                return false;
            } else
            {
                int selectedIndex = UnityEngine.Random.Range(0, packages.Count);
                Package selectedPackage = packages[selectedIndex];

                GoapAgent.worldStates.AddState("isPackageReserved", 3);
                deliveryService.ReservePackage(selectedPackage, GoapAgent.Parent);

                //target = selectedPackage.gameObject.transform.position;

                return true;
            }
        }

        public override bool PrePerform()
        {
            return true;
        }

        protected override AIState[] GetAfterEffects()
        {
            return new AIState[] { new AIState("isPackageReserved", 3) };
        }

        protected override AIState[] GetPreConditions()
        {
            return new AIState[0];
        }

        public override GoapAction<Biker> Clone(GoapAgent<Biker> agent = null)
        {
            var action = new ReservePackageAction(packageStore, deliveryService);
            action.agent = agent;
            return action;
        }
    }
}
