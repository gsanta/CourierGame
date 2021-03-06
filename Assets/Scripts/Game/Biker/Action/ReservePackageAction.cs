using AI;
using System.Collections.Generic;
using Delivery;

namespace GameObjects
{
    public class ReservePackageAction : GoapAction<GameCharacter>
    {

        private PackageStore packageStore;
        private readonly DeliveryService deliveryService;

        public ReservePackageAction(PackageStore packageStore, DeliveryService deliveryService) : base(new AIStateName[] { }, new AIStateName[] { AIStateName.PACKAGE_IS_RESERVED })
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

                GoapAgent.worldStates.AddStates(GetAfterEffects());
                deliveryService.ReservePackage(selectedPackage, GoapAgent.Parent);

                //target = selectedPackage.gameObject.transform.position;

                return true;
            }
        }

        public override bool PrePerform()
        {
            return true;
        }
        public override GoapAction<GameCharacter> Clone(GoapAgent<GameCharacter> agent = null)
        {
            var action = new ReservePackageAction(packageStore, deliveryService);
            action.agent = agent;
            return action;
        }
    }
}
