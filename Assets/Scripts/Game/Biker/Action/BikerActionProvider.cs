
using AI;
using System.Collections.Generic;
using System.Linq;

namespace Bikers
{
    public class BikerActionProvider
    {
        private List<GoapAction<Biker>> actions = new List<GoapAction<Biker>>();

        public void AddAction(GoapAction<Biker> action)
        {
            actions.Add(action);
        }

        public List<GoapAction<Biker>> CloneActions(GoapAgent<Biker> agent)
        {
            return actions.Select(action => action.CloneAndSetup(agent)).ToList();
        }
    }
}
