using AI;
using UnityEngine;

namespace Bikers
{
    public struct BikerConfig
    {
        public readonly GameObject spawnPoint;
        public readonly Goal goal;
        public readonly string name;

        public BikerConfig(GameObject spawnPoint, Goal goal, string name)
        {
            this.spawnPoint = spawnPoint;
            this.goal = goal;
            this.name = name;
        }
    }
}
