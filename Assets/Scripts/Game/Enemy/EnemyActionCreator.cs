using Agents;
using AI;
using GameObjects;
using System.Collections.Generic;
using System.Linq;

namespace Enemies
{
    public class EnemyActionCreator : IActionCreator<GameCharacter>
    {
        private List<GoapAction<GameCharacter>> actions = new List<GoapAction<GameCharacter>>();

        public void SetActions(List<GoapAction<GameCharacter>> actions)
        {
            this.actions = actions;
        }

        public List<GoapAction<GameCharacter>> GetActions()
        {
            return actions.Select(action => action.Clone()).ToList();
        }
    }
}
