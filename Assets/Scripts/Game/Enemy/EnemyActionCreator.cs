using Agents;
using AI;
using Pedestrians;
using System.Collections.Generic;
using System.Linq;

namespace Enemies
{
    public class EnemyActionCreator : IActionCreator<Enemy>
    {
        private readonly WalkAction<Enemy> walkAction;
        private readonly WalkTargetStore walkTargetStore;
        private List<GoapAction<Enemy>> actions = new List<GoapAction<Enemy>>();

        public EnemyActionCreator(WalkTargetStore walkTargetStore, WalkAction<Enemy> walkAction)
        {
            this.walkTargetStore = walkTargetStore;
            this.walkAction = walkAction;
            Init();
        }

        public List<GoapAction<Enemy>> GetActions()
        {
            return actions.Select(action => action.Clone()).ToList();
        }

        public void Init()
        {
            walkTargetStore.GetTargets().ForEach(target =>
            {
                var clone = (WalkAction<Enemy>)walkAction.Clone();
                clone.SetHideDuration(target.hideDuration);
                actions.Add(clone);
            });
        }
    }
}
