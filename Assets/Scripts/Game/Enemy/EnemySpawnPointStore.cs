
using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawnPointStore : ITargetStore
    {
        private List<GameObject> spawnPoints;

        public void SetTargets(List<GameObject> targets)
        {
            spawnPoints = targets;
        }
    }
}
