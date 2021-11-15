using Actions;
using Pedestrians;
using RSG;

namespace GamePlay
{
    public class PedestrianTurns : ITurns
    {
        private PedestrianStore pedestrianStore;
        private readonly ActionFactory actionFactory;

        public PedestrianTurns(PedestrianStore pedestrianStore, ActionFactory actionFactory)
        {
            this.pedestrianStore = pedestrianStore;
            this.actionFactory = actionFactory;
        }

        public void Reset()
        {
            pedestrianStore.GetAll().ForEach(pedestrian => pedestrian.Agent.AbortAction());
        }

        public Promise Execute()
        {
            pedestrianStore.GetAll().ForEach(pedestrian => pedestrian.Agent.SetActions(actionFactory.CreatePedestrianWalkAction(pedestrian.Agent)));
            return (Promise) Promise.Resolved();
        }

        public void Step()
        {
        }
    }
}
