
using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawnPointStore : ITargetStore<GameObject>
    {
        private List<GameObject> spawnPoints;

        public void SetTargets(List<GameObject> targets)
        {
            spawnPoints = targets;
        }

        public GameObject GetRandomSpawnPoint()
        {
            int index = Random.Range(0, spawnPoints.Count - 1);
            return spawnPoints[index];
        }
    }
}
