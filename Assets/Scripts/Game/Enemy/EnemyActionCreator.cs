using Agents;
using AI;
using Pedestrians;
using System.Collections.Generic;
using System.Linq;

namespace Enemies
{
    public class EnemyActionCreator : IActionCreator<Enemy>
    {
        private List<GoapAction<Enemy>> actions = new List<GoapAction<Enemy>>();

        public void SetActions(List<GoapAction<Enemy>> actions)
        {
            this.actions = actions;
        }

        public List<GoapAction<Enemy>> GetActions()
        {
            return actions.Select(action => action.Clone()).ToList();
        }
    }
}
