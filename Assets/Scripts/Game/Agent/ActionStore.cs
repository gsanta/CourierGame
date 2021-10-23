
using AI;
using Bikers;
using Core;
using Pedestrians;
using System.Collections.Generic;
using System.Linq;

namespace Agents
{
    public class ActionStore : IResetable
    {
        private readonly PedestrianTargetStore pedestrianTargetStore;
        private List<GoapAction<Pedestrian>> walkActions = new List<GoapAction<Pedestrian>>();

        public ActionStore(PedestrianTargetStore pedestrianTargetStore)
        {
            this.pedestrianTargetStore = pedestrianTargetStore;
        }

        public PickUpPackageAction PickUpPackageAction { get; set; }

        public WalkAction WalkAction
        {
            get; set;
        }

        public List<GoapAction<Pedestrian>> GetPedestrianActions()
        {
            return walkActions.Select(action => action.Clone()).ToList();
        }

        public WalkAction GetByAfterEffect(string afterEffectName)
        {
            return (WalkAction) walkActions.Find(action => action.afterEffect.key == afterEffectName).Clone();
        }

        public void Init()
        {

            pedestrianTargetStore.GetTargets().ForEach(target =>
            {
                var clone = (WalkAction)WalkAction.Clone();
                clone.SetTarget(target.gameObject).SetHideDuration(target.hideDuration).SetAfterEffect(new WorldState("goto" + target.name, 3));
                walkActions.Add(clone);
            });
        }

        public void Reset()
        {
            walkActions = new List<GoapAction<Pedestrian>>();
        }
    }
}
