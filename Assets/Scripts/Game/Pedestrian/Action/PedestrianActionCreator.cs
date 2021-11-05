using Agents;
using AI;
using System.Collections.Generic;
using System.Linq;

namespace Pedestrians
{
    public class PedestrianActionCreator : IActionCreator<Pedestrian>
    {
        private readonly WalkAction<Pedestrian> walkAction;
        private readonly GoHomeAction goHomeAction;
        private readonly WalkTargetStore walkTargetStore;
        private List<GoapAction<Pedestrian>> actions = new List<GoapAction<Pedestrian>>();

        public PedestrianActionCreator(WalkTargetStore walkTargetStore,  WalkAction<Pedestrian> walkAction, GoHomeAction goHomeAction)
        {
            this.walkTargetStore = walkTargetStore;
            this.walkAction = walkAction;
            this.goHomeAction = goHomeAction;

            actions.Add(walkAction);
            actions.Add(goHomeAction);
            Init();
        }

        public List<GoapAction<Pedestrian>> GetActions()
        {
            return actions.Select(action => action.Clone()).ToList();
        }

        public void Init()
        {
            walkTargetStore.GetTargets().ForEach(target =>
            {
                var clone = (WalkAction<Pedestrian>) walkAction.Clone();
                clone.SetHideDuration(target.hideDuration);
                actions.Add(clone);
            });
        }
    }
}
