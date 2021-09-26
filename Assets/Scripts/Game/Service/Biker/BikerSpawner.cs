using Model;
using AI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Bikers;

namespace Service
{
    public class BikerSpawner : BaseSpawner<BikerConfig>
    {
        private BikerStore bikerStore;
        private BikerFactory bikerFactory;

        public BikerSpawner(BikerFactory bikerFactory, BikerStore bikerStore)
        {
            this.bikerFactory = bikerFactory;
            this.bikerStore = bikerStore;
        }

        override public void StartSpawning()
        {
            base.StartSpawning();
        }

        public void Spawn()
        {
            List<GameObject> usedSpawnPoints = new List<GameObject>();

            for (int i = 0; i < 1; i++)
            {
                //GameObject spawnPoint = ChooseSpawnPoint(usedSpawnPoints);
                GameObject spawnPoint = bikerStore.SpawnPoints[2];
                BikerConfig config = new BikerConfig(spawnPoint, new SubGoal("isPackageDropped", 1, true), $"Courier-{i}");
                Biker courier = bikerFactory.Create(config);
                bikerStore.Add(courier);
            }
        }

        private GameObject ChooseSpawnPoint(List<GameObject> usedSpawnPoints)
        {
            var freeSpawnPoints = bikerStore.SpawnPoints.Where(spawnPoint => usedSpawnPoints.Contains(spawnPoint) == false).ToArray();
            int randomIndex = Random.Range(0, freeSpawnPoints.Length);

            var spawnPoint = freeSpawnPoints[randomIndex];

            usedSpawnPoints.Add(spawnPoint);

            return spawnPoint;
        }
    }
}