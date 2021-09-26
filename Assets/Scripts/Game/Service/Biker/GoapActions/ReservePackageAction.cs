using Model;
using AI;
using System.Collections.Generic;

namespace Service
{
    public class ReservePackageAction : GoapAction<Biker>
    {

        private PackageStore packageStore;
        private readonly IDeliveryService deliveryService;

        public ReservePackageAction(IGoapAgentProvider<Biker> goapAgentProvider, PackageStore packageStore, IDeliveryService deliveryService) : base(goapAgentProvider)
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
                Package selectedPackage = packages[0];

                deliveryService.ReservePackage(selectedPackage, GoapAgent.Parent);

                target = selectedPackage.gameObject.transform.position;

                return true;
            }
        }

        public override bool PrePerform()
        {
            return true;
        }

        protected override WorldState[] GetAfterEffects()
        {
            return new WorldState[] { new WorldState("isPackageReserved", 3) };
        }

        protected override WorldState[] GetPreConditions()
        {
            return new WorldState[0];
        }
    }
}
