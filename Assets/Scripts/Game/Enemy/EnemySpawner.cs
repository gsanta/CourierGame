namespace Enemies
{
    public class EnemySpawner
    {
        private EnemyStore enemyStore;
        private EnemyFactory enemyFactory;
        private EnemySpawnPointStore enemySpawnPointStore;
        private EnemiesConfig enemiesConfig;

        public EnemySpawner(EnemyStore enemyStore, EnemyFactory enemyFactory, EnemySpawnPointStore enemySpawnPointStore, EnemiesConfig enemiesConfig)
        {
            this.enemyStore = enemyStore;
            this.enemyFactory = enemyFactory;
            this.enemySpawnPointStore = enemySpawnPointStore;
            this.enemiesConfig = enemiesConfig;
        }

        public void Spawn()
        {
            for (int i = 0; i < enemiesConfig.EnemyCount; i++)
            {
                var spawnPoint = enemySpawnPointStore.GetRandomSpawnPoint();

                var pedestrian = enemyFactory.Create(new EnemyData(spawnPoint));
                pedestrian.gameObject.SetActive(true);
                enemyStore.Add(pedestrian);

            }
        }
    }
}
