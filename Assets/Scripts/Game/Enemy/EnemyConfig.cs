using UnityEngine;

namespace Enemies
{
    public struct EnemyConfig
    {
        public readonly GameObject spawnPoint;

        public EnemyConfig(GameObject spawnPoint)
        {
            this.spawnPoint = spawnPoint;
        }
    }
}
