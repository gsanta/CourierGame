
using Core;
using System.Collections.Generic;

namespace Pedestrians
{
    public class PedestrianTargetStore : IClearable
    {
        private List<PedestrianTarget> targets = new List<PedestrianTarget>();

        public List<PedestrianTarget> GetTargets()
        {
            return targets;
        }

        public void SetTargets(List<PedestrianTarget> targets)
        {
            this.targets = targets;
        }

        public PedestrianTarget GetByName(string name)
        {
            return targets.Find(goal => goal.name == name);
        }

        public void Clear()
        {
            targets = new List<PedestrianTarget>();
        }
    }
}
