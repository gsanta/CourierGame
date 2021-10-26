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

            while (count < enemyCount)
            {

                var waypoint = enemyStore.GetWaypoints()[Random.Range(0, pavementStore.GetWaypoints().Count - 1)];

                var pedestrian = pedestrianFactory.Create(new PedestrianConfig(waypoint.gameObject));
                pedestrian.gameObject.SetActive(true);
                pedestrianStore.Add(pedestrian);

                count++;
            }
        }
    }
}
