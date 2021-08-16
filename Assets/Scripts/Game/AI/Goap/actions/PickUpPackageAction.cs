using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class PickUpPackageAction : GAction
    {
        public override bool PrePerform()
        {
            List<Package> packages = packageStore.GetAllPickable();

            if (packages.Count == 0)
            {
                return false;
            }

            int selectedIndex = UnityEngine.Random.Range(0, packages.Count);
            Package selectedPackage = packages[selectedIndex];

            CourierAgent agent = GetComponent<CourierAgent>();

            selectedPackage.PickupBy(agent);

            target = selectedPackage.gameObject;

            return true;
        }
        public override bool PostPerform()
        {
            return true;
        }
    }
}
