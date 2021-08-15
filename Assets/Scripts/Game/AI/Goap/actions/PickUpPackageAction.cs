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
            return true;
        }
        public override bool PostPerform()
        {
            return true;
        }
    }
}
