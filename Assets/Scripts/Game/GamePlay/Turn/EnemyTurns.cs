using Actions;
using Enemies;
using RSG;

namespace GamePlay
{
    public class EnemyTurns : ITurns
    {
        private EnemyStore enemyStore;
        private readonly ActionFactory actionFactory;
        private readonly GameObjectStore gameObjectStore;
        private Promise promise;

        public EnemyTurns(EnemyStore enemyStore, ActionFactory actionFactory, GameObjectStore gameObjectStore)
        {
            this.enemyStore = enemyStore;
            this.actionFactory = actionFactory;
            this.gameObjectStore = gameObjectStore;
        }


        public Promise Execute()
        {
            promise = new Promise();
            enemyStore.GetAll().ForEach(pedestrian => pedestrian.Agent.Active = true);
            enemyStore.GetAll().ForEach(enemy => {
                enemy.Agent.NavMeshAgent.isStopped = false;

                enemy.Agent.SetActions(actionFactory.CreateEnemyWalkAction(enemy.Agent));
                enemy.Agent.SetGoals(enemy.GetGoalProvider().CreateGoal(), false);
            });
            gameObjectStore.InvokeHelper.GetMonoBehaviour().Invoke("InvokeEnemyTurns", 5);
            return promise;
        }

        public void Reset()
        {
            enemyStore.GetAll().ForEach(pedestrian => pedestrian.Agent.AbortAction());
        }
        public void Invoke()
        {
            enemyStore.GetAll().ForEach(pedestrian => pedestrian.Agent.AbortAction());
            promise.Resolve();
        }

        public void Step()
        {

        }
    }
}
