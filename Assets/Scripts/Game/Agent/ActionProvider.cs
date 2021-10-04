
using AI;
using Bikers;
using Pedestrians;
using System.Collections.Generic;

namespace Agents
{
    public class ActionProvider
    {
        private readonly PedestrianGoalStore pedestrianGoalStore;
        private readonly GoalProvider goalProvider;
        private List<WalkAction> walkActions = new List<WalkAction>();

        public ActionProvider(PedestrianGoalStore pedestrianGoalStore, GoalProvider goalProvider)
        {
            this.pedestrianGoalStore = pedestrianGoalStore;
            this.goalProvider = goalProvider;
        }

        public PickUpPackageAction PickUpPackageAction { get; set; }

        public WalkAction WalkAction
        {
            get; set;
        }

        public void Init()
        {
            goalProvider.GetGoals().ForEach(goal =>
            {
                var clone = (WalkAction) WalkAction.Clone();
                
                clone.SetTarget(pedestrianGoalStore.GetByName(goal.targetName)).SetHideDuration(goal.hideDuration).SetAfterEffect(new WorldState(goal.goalName, 3));

                walkActions.Add(clone);
            });
        }
    }
}
