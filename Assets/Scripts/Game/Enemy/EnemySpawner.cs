using Route;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawner
    {
        private int enemyCount;

        private EnemyStore enemyStore;
        private EnemyFactory enemyFactory;

        public EnemySpawner(EnemyStore enemyStore, EnemyFactory enemyFactory)
        {
            this.enemyStore = enemyStore;
            this.enemyFactory = enemyFactory;
        }

        public void SetEnemyCount(int enemyCount)
        {
            this.enemyCount = enemyCount;
        }

        public void Spawn()
        {
            int count = 0;

            while (count < 3)
            {
                var waypoint = pavementStore.GetWaypoints()[Random.Range(0, pavementStore.GetWaypoints().Count - 1)];

                var pedestrian = enemyFactory.Create(new EnemyData(waypoint.gameObject));
                pedestrian.gameObject.SetActive(true);
                enemyStore.Add(pedestrian);

                count++;
            }
        }
    }
}
