
using Enemies;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class EnemyInstantiator : MonoBehaviour, IEnemyInstantiator
    {
        private EnemiesConfig enemiesConfig;

        [Inject]
        public void Construct(EnemiesConfig enemiesConfig, EnemyFactory enemyFactory)
        {
            this.enemiesConfig = enemiesConfig;
            enemyFactory.SetPedestrianInstantiator(this);
        }

        public Enemy InstantiateEnemy()
        {
            return Instantiate(enemiesConfig.enemyTemplate, enemiesConfig.enemyContainer.transform);
        }
    }
}
