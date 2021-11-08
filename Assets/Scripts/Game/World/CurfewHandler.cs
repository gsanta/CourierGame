
using Pedestrians;
using UI;

namespace Worlds
{
    public class CurfewHandler : IWorldStateChangeHandler
    {
        private IMapState worldState;
        private PedestrianStore pedestrianStore;
        private readonly CurfewButton curfewButton;
        private PedestrianGoalFactory pedestrianGoalFactory;

        public CurfewHandler(PedestrianStore pedestrianStore, PedestrianGoalFactory pedestrianGoalFactory, CurfewButton curfewButton)
        {
            this.pedestrianStore = pedestrianStore;
            this.curfewButton = curfewButton;
            this.pedestrianGoalFactory = pedestrianGoalFactory;
        }

        public void SetWorldState(IMapState worldState)
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
            pedestrianStore.GetAll().ForEach(pedestrian => pedestrianGoalFactory.GoHome(pedestrian));
            curfewButton.UpdateColor();
        }

        private void CurfewOff()
        {
            pedestrianStore.GetAll().ForEach(pedestrian => pedestrian.Agent.SetGoals(pedestrian.GoalProvider.CreateGoal(), true));
            curfewButton.UpdateColor();
        }
    }
}
