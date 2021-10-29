
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
        private readonly WalkTargetStore walkTargetStore;
        private List<GoapAction<Pedestrian>> pedestrianActions = new List<GoapAction<Pedestrian>>();

        public ActionStore(WalkTargetStore walkTargetStore)
        {
            this.walkTargetStore = walkTargetStore;
        }

        public PickUpPackageAction PickUpPackageAction { get; set; }

        public void AddPedestrianAction(GoapAction<Pedestrian> pedestrianAction)
        {
            pedestrianActions.Add(pedestrianAction);
        }

        public WalkAction WalkAction
        {
            get; set;
        }

        public List<GoapAction<Pedestrian>> GetPedestrianActions()
        {
            return pedestrianActions.Select(action => action.Clone()).ToList();
        }

        public GoapAction<Pedestrian> GetByAfterEffect(string afterEffectName)
        {
            return pedestrianActions.Find(action => action.afterEffect.key == afterEffectName).Clone();
        }

        public void Init()
        {

            walkTargetStore.GetTargets().ForEach(target =>
            {
                var clone = (WalkAction)WalkAction.Clone();
                clone.SetTarget(target.gameObject).SetHideDuration(target.hideDuration).SetAfterEffect(new AIState("goto" + target.name, 3));
                pedestrianActions.Add(clone);
            });
        }

        public void Reset()
        {
            pedestrianActions = new List<GoapAction<Pedestrian>>();
        }
    }
}
