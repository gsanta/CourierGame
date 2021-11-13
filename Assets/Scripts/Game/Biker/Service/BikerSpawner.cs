using AI;
using System.Collections.Generic;
using UnityEngine;

namespace Bikers
{
    public class BikerSpawner : BaseSpawner<BikerConfig>
    {
        private BikerStore bikerStore;
        private BikerFactory bikerFactory;
        private BikerHomeStore bikerHomeStore;
        private BikersConfig bikersConfig;

        public BikerSpawner(BikerFactory bikerFactory, BikerStore bikerStore, BikerHomeStore bikerHomeStore, BikersConfig bikersConfig)
        {
            this.bikerFactory = bikerFactory;
            this.bikerStore = bikerStore;
            this.bikerHomeStore = bikerHomeStore;
            this.bikersConfig = bikersConfig;
        }

        override public void StartSpawning()
        {
            base.StartSpawning();
        }

        public void Spawn()
        {
            List<GameObject> spawnPoints = bikerHomeStore.GetHomes();
            for (int i = 0; i < bikersConfig.BikerCount; i++)
            {
                BikerConfig config = new BikerConfig(spawnPoints[i], new Goal(AIStateName.PACKAGE_IS_DROPPED, false), $"Player-{i}");
                Biker courier = bikerFactory.Create(config);
                bikerStore.Add(courier);
            }
        }
    }
}