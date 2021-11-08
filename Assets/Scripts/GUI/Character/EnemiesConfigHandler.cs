
using Enemies;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class EnemiesConfigHandler : MonoBehaviour
    {
        [SerializeField]
        private int enemyCount;
        [SerializeField]
        private Enemy enemyTemplate;
        [SerializeField]
        private GameObject enemyContainer;
        [SerializeField]
        private GameObject enemySpawnPointContainer;

        private EnemiesConfig enemiesConfig;
        private StoreSetup storeSetup;
        private EnemySpawnPointStore enemySpawnPointStore;

        [Inject]
        public void Construct(EnemiesConfig enemiesConfig, EnemySpawnPointStore enemySpawnPointStore, StoreSetup storeSetup)
        {
            this.enemiesConfig = enemiesConfig;
            this.enemySpawnPointStore = enemySpawnPointStore;
            this.storeSetup = storeSetup;
        }

        private void Awake()
        {
            enemiesConfig.enemyTemplate = enemyTemplate;
            enemiesConfig.enemyContainer = enemyContainer;
            enemiesConfig.EnemyCount = enemyCount;
            storeSetup.SetupStoreWithGameObjects(enemySpawnPointContainer, enemySpawnPointStore);
        }
    }
}
