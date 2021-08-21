using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    public class AssignPackageAction : GAction
    {

        private PackageStore packageStore;

        public AssignPackageAction(GAgent agent, PackageStore packageStore) : base(agent)
        {
            this.packageStore = packageStore;
        }

        public override bool IsDestinationReached()
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

                selectedPackage.ReservePackage((CourierAgent) agent);

                target = selectedPackage.gameObject;

                return true;
            }
        }

        public override bool PrePerform()
        {
            return true;
        }

        protected override WorldState[] GetAfterEffects()
        {
            return new WorldState[] { new WorldState("isPackageAssigned", 3) };
        }

        protected override WorldState[] GetPreConditions()
        {
            return new WorldState[0];
        }
    }
}
