using UnityEngine;

namespace Model
{
    public struct PackageConfig
    {
        public readonly GameObject spawnPoint;
        public readonly int price;

        public PackageConfig(GameObject spawnPoint, int price)
        {
            this.spawnPoint = spawnPoint;
            this.price = price;
        }
    }
}
