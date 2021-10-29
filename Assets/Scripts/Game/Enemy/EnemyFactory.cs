using AI;
using Bikers;

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

            //Transform child = config.spawnPoint.transform;
            //enemy.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            //enemy.transform.position = child.position;

            return enemy;
        }

        public void InitializeObj(Enemy enemy)
        {
            enemy.transform.position = enemy.GetComponent<WaypointNavigator>().currentWaypoint.transform.position;
        }
    }
}
