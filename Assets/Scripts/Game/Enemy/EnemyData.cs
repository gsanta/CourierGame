using UnityEngine;

namespace Enemies
{
    public struct EnemyData
    {
        public readonly GameObject spawnPoint;

        public EnemyData(GameObject spawnPoint)
        {
            this.spawnPoint = spawnPoint;
        }
    }
}
