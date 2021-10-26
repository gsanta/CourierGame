using AI;
using UnityEngine;

namespace Bikers
{
    public class BikerSpawner : BaseSpawner<BikerConfig>
    {
        private BikerStore bikerStore;
        private BikerFactory bikerFactory;
        private BikerHomeStore bikerHomeStore;

        public BikerSpawner(BikerFactory bikerFactory, BikerStore bikerStore, BikerHomeStore bikerHomeStore)
        {
            this.bikerFactory = bikerFactory;
            this.bikerStore = bikerStore;
            this.bikerHomeStore = bikerHomeStore;
        }

        override public void StartSpawning()
        {
            base.StartSpawning();
        }

        public void Spawn()
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject spawnPoint = bikerHomeStore.ChooseHome();
                BikerConfig config = new BikerConfig(spawnPoint, new SubGoal("isPackageDropped", 1, true), $"Courier-{i}");
                Biker courier = bikerFactory.Create(config);
                bikerStore.Add(courier);
            }
        }
    }
}