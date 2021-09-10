
using UnityEngine;

namespace Domain
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
