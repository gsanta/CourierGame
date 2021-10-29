using Core;
using System.Collections.Generic;

namespace Pedestrians
{
    public class WalkTargetStore : IResetable
    {
        private List<walkTarget> targets = new List<walkTarget>();

        public List<walkTarget> GetTargets()
        {
            return targets;
        }

        public void SetTargets(List<walkTarget> targets)
        {
            this.targets = targets;
        }

        public walkTarget GetByName(string name)
        {
            return targets.Find(goal => goal.name == name);
        }

        public void Reset()
        {
            targets = new List<walkTarget>();
        }
    }
}
