
using AI;
using Pedestrians;
using System.Collections.Generic;

namespace Agents
{
    public class ActionFeeder
    {
        private readonly PedestrianGoalStore pedestrianGoalStore;
        private readonly ActionProvider actionStore;

        private List<WalkAction> walkActions = new List<WalkAction>();

        public ActionFeeder(PedestrianGoalStore pedestrianGoalStore, ActionProvider actionStore)
        {
            this.pedestrianGoalStore = pedestrianGoalStore;
            this.actionStore = actionStore;
        }

        public GoapAction<Pedestrian> FeedPedestrianAction() {
            return null;
        }

        public void Init()
        {
            var walkAction = actionStore.WalkAction;

            walkActions.Add(walkAction.SetTarget(pedestrianGoalStore.GetByName("House 1")).SetHideDuration(2000));
        }
    }
}
