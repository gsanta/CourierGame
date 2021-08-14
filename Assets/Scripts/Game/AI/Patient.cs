using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    public class Patient : GAgent
    {

        public new void Start()
        {
            base.Start();
            SubGoal s1 = new SubGoal("isWaiting", 1, true);
            goals.Add(s1, 3);
        }
    }
}
