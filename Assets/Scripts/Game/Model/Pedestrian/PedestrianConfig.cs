using UnityEngine;

namespace Model
{
    public struct PedestrianConfig
    {
        public readonly GameObject spawnPoint;

        public PedestrianConfig(GameObject spawnPoint)
        {
            this.spawnPoint = spawnPoint;
        }
    }
}
