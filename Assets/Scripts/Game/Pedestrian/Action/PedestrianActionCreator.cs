using Agents;
using AI;
using System.Collections.Generic;
using System.Linq;

namespace Pedestrians
{
    public class PedestrianActionCreator : IActionCreator<Pedestrian>
    {
        private List<GoapAction<Pedestrian>> actions = new List<GoapAction<Pedestrian>>();

        public void SetActions(List<GoapAction<Pedestrian>> actions)
        {
            this.actions = actions;
        }

        public List<GoapAction<Pedestrian>> GetActions()
        {
            return actions.Select(action => action.Clone()).ToList();
        }
    }
}
