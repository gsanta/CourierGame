using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class DeliverPackageAction : GAction
    {
        public override bool PrePerform()
        {
            CourierAgent agent = GetComponent<CourierAgent>();
            Package package = agent.GetPackage();

            target = package.Target.gameObject;

            return true;
        }

        public override bool PostPerform()
        {

            CourierAgent agent = GetComponent<CourierAgent>();
            Package package = agent.GetPackage();

            package.DropPackage();

            SubGoal s1 = new SubGoal("isPackageDropped", 1, true);
            agent.goals.Add(s1, 3);

            return true;
        }
    }
}
