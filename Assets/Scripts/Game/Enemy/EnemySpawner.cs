using Route;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawner
    {
        private int enemyCount;

        private EnemyStore enemyStore;
        private EnemyFactory enemyFactory;
        private EnemySpawnPointStore enemySpawnPointStore;

        public EnemySpawner(EnemyStore enemyStore, EnemyFactory enemyFactory, EnemySpawnPointStore enemySpawnPointStore)
        {
            this.enemyStore = enemyStore;
            this.enemyFactory = enemyFactory;
            this.enemySpawnPointStore = enemySpawnPointStore;
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
                var spawnPoint = enemySpawnPointStore.GetRandomSpawnPoint();

                var pedestrian = enemyFactory.Create(new EnemyData(spawnPoint));
                pedestrian.gameObject.SetActive(true);
                enemyStore.Add(pedestrian);

                count++;
            }
        }
    }
}
