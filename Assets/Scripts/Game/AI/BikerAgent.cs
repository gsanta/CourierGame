using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    public class BikerAgent : GAgent
    {

        public new void Start()
        {
            base.Start();
            SubGoal s1 = new SubGoal("isPackagePickedUp", 1, true);
            goals.Add(s1, 3);
        }
    }
}
