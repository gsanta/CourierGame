
using AI;
using Pedestrians;
using System.Collections.Generic;

namespace Worlds
{
    public class CurfewHandler : IWorldStateChangeHandler
    {
        private IWorldState worldState;
        private PedestrianStore pedestrianStore;

        public CurfewHandler(PedestrianStore pedestrianStore)
        {
            this.pedestrianStore = pedestrianStore;
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
        }

        private void CurfewOff()
        {

        }
    }
}
