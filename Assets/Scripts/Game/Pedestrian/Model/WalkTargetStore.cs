using Core;
using System.Collections.Generic;

namespace Pedestrians
{
    public class WalkTargetStore : IResetable, ITargetStore<WalkTarget>
    {
        private List<WalkTarget> targets = new List<WalkTarget>();

        public List<WalkTarget> GetTargets()
        {
            return targets;
        }

        public WalkTarget GetByName(string name)
        {
            return targets.Find(goal => goal.name == name);
        }

        public void Reset()
        {
            targets = new List<WalkTarget>();
        }

        public void SetTargets(List<WalkTarget> targets)
        {
            this.targets = targets;
        }
    }
}
