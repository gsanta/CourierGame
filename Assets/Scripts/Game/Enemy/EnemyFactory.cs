using Agents;
using AI;
using Bikers;
using Pedestrians;
using UnityEngine;

namespace Enemies
{
    public class EnemyFactory : ItemFactory<EnemyData, Enemy>
    {
        private AgentFactory agentFactory;
        private IEnemyInstantiator enemyInstantiator;
        private WalkTargetStore walkTargetStore;

        public EnemyFactory(AgentFactory agentFactory, WalkTargetStore walkTargetStore)
        {
            this.agentFactory = agentFactory;
            this.walkTargetStore = walkTargetStore;
        }

        public void SetPedestrianInstantiator(IEnemyInstantiator enemyInstantiator)
        {
            this.enemyInstantiator = enemyInstantiator;
        }

        public Enemy Create(EnemyData config)
        {
            Enemy enemy = enemyInstantiator.InstantiateEnemy();
            enemy.Agent = agentFactory.CreateEnemyAgent(enemy);
            enemy.GoalProvider = new EnemyGoalProvider(enemy, walkTargetStore);
            enemy.Agent.SetGoals(enemy.GoalProvider.CreateGoal(), false);

            Transform transform = config.spawnPoint.transform;
            enemy.transform.position = transform.position;

            return enemy;
        }

        public void InitializeObj(Enemy enemy)
        {
            enemy.transform.position = enemy.GetComponent<WaypointNavigator>().currentWaypoint.transform.position;
        }
    }
}
