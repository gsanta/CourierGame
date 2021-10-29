using AI;
using Bikers;
using UnityEngine;

namespace Enemies
{
    public class EnemyFactory : ItemFactory<EnemyData, Enemy>
    {
        private AgentFactory agentFactory;
        private IEnemyInstantiator enemyInstantiator;

        public EnemyFactory(AgentFactory agentFactory)
        {
            this.agentFactory = agentFactory;
        }

        public void SetPedestrianInstantiator(IEnemyInstantiator enemyInstantiator)
        {
            this.enemyInstantiator = enemyInstantiator;
        }

        public Enemy Create(EnemyData config)
        {
            Enemy enemy = enemyInstantiator.InstantiateEnemy();
            enemy.agent = agentFactory.CreateEnemyAgent(enemy);

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
