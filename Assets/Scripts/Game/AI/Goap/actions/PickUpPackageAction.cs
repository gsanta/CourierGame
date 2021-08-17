using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
            Package selectedPackage = packages[0];

            CourierAgent agent = GetComponent<CourierAgent>();

            selectedPackage.ReservePackage(agent);

            target = selectedPackage.gameObject;
            Debug.Log(target.transform.position);

            return true;
        }
        public override bool PostPerform()
        {
            CourierAgent agent = GetComponent<CourierAgent>();

            Package package = agent.GetPackage();
            package.PickupBy(agent);
            return true;
        }
    }
}
