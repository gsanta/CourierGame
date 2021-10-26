
using AI;
using Pedestrians;
using System.Collections.Generic;
using UI;

namespace Worlds
{
    public class CurfewHandler : IWorldStateChangeHandler
    {
        private IWorldState worldState;
        private PedestrianStore pedestrianStore;
        private readonly CurfewButton curfewButton;

        public CurfewHandler(PedestrianStore pedestrianStore, CurfewButton curfewButton)
        {
            this.pedestrianStore = pedestrianStore;
            this.curfewButton = curfewButton;
        }

        public void SetWorldState(IWorldState worldState)
        {
            this.worldState = worldState;
        }

        public void StateChanged()
        {
            if (worldState.Curfew)
            {
                CurfewOn();
            } else
            {
                CurfewOff();
            }
        }

        private void CurfewOn()
        {
            pedestrianStore.GetAll().ForEach(pedestrian => pedestrian.Agent.SetGoals(new List<SubGoal>() { new SubGoal("atHome", 3, true) }, true));
            curfewButton.UpdateColor();
        }

        private void CurfewOff()
        {
            pedestrianStore.GetAll().ForEach(pedestrian => pedestrian.Agent.SetGoals(pedestrian.GoalProvider.CreateWalkGoal(), true));
            curfewButton.UpdateColor();
        }
    }
}
