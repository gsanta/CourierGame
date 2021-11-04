﻿
using Enemies;
using UnityEngine;

namespace Controls
{
    public class EnemiesConfigController : MonoBehaviour
    {
        [SerializeField]
        private Enemy enemyTemplate;
        [SerializeField]
        private GameObject enemyContainer;

        public void SetupConfig(EnemiesConfig enemiesConfig)
        {
            enemiesConfig.enemyTemplate = enemyTemplate;
            enemiesConfig.enemyContainer = enemyContainer;
        }
    }
}
