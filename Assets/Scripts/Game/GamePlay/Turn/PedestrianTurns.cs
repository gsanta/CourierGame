using Actions;
using Pedestrians;
using RSG;

namespace GamePlay
{
    public class PedestrianTurns : ITurns
    {
        private PedestrianStore pedestrianStore;
        private readonly ActionFactory actionFactory;
        private readonly GameObjectStore gameObjectStore;
        private Promise promise;

        public PedestrianTurns(PedestrianStore pedestrianStore, ActionFactory actionFactory, GameObjectStore gameObjectStore)
        {
            this.pedestrianStore = pedestrianStore;
            this.actionFactory = actionFactory;
            this.gameObjectStore = gameObjectStore;
        }

        public void Reset()
        {
            pedestrianStore.GetAll().ForEach(pedestrian => pedestrian.Agent.AbortAction());
        }

        public Promise Execute()
        {
            promise = new Promise();
            pedestrianStore.GetAll().ForEach(pedestrian => pedestrian.Agent.Active = true);
            pedestrianStore.GetAll().ForEach(pedestrian => {
                pedestrian.Agent.NavMeshAgent.isStopped = false;

                pedestrian.Agent.SetActions(actionFactory.CreatePedestrianWalkAction(pedestrian.Agent));
                pedestrian.Agent.SetGoals(pedestrian.GetGoalProvider().CreateGoal(), false);
            });
            gameObjectStore.InvokeHelper.GetMonoBehaviour().Invoke("InvokePedestrianTurns", 5);
            return promise;
        }

        public void Invoke()
        {
            pedestrianStore.GetAll().ForEach(pedestrian => pedestrian.Agent.AbortAction());
            promise.Resolve();
        }

        public void Step()
        {
        }

        public void Pause()
        {
            throw new System.NotImplementedException();
        }

        public void Resume()
        {
            throw new System.NotImplementedException();
        }

        public void Abort()
        {
            throw new System.NotImplementedException();
        }
    }
}
