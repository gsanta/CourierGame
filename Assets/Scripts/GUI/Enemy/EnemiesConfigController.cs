
using Enemies;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class EnemiesConfigController : MonoBehaviour
    {
        [SerializeField]
        private Enemy enemyTemplate;
        [SerializeField]
        private GameObject enemyContainer;
        [SerializeField]
        private int enemyCount;
        private EnemiesConfig enemiesConfig;

        [Inject]
        public void Construct(EnemiesConfig enemiesConfig)
        {
            this.enemiesConfig = enemiesConfig;
        }

        private void Awake()
        {
            enemiesConfig.enemyTemplate = enemyTemplate;
            enemiesConfig.enemyContainer = enemyContainer;
            enemiesConfig.EnemyCount = enemyCount;
        }
    }
}
