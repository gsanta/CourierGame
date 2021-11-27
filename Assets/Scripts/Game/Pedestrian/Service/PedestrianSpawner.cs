using Route;
using UnityEngine;
using Worlds;
using Zenject;

namespace Pedestrians
{
    public class PedestrianSpawner
    {
        private int pedestrianCount;

        private PedestrianStore pedestrianStore;
        private PedestrianFactory pedestrianFactory;
        private RoadStore roadStore;
        private WorldStore worldStore;

        public PedestrianSpawner(PedestrianStore pedestrianStore, PedestrianFactory pedestrianFactory, RoadStore roadStore, WorldStore worldStore)
        {
            this.pedestrianStore = pedestrianStore;
            this.pedestrianFactory = pedestrianFactory;
            this.roadStore = roadStore;
            this.worldStore = worldStore;
        }
        
        public void SetPedestrianCount(int pedestrianCount)
        {
            this.pedestrianCount = pedestrianCount;
        }

        public void Spawn()
        {
            int count = 0;

            while (count < pedestrianCount)
            {
                var road = roadStore.GetRoad(worldStore.CurrentMap);
                var node = road.GetNodes()[Random.Range(0, road.GetNodes().Count - 1)];

                var pedestrian = pedestrianFactory.Create(new PedestrianConfig(node.GetMonoBehaviour().gameObject));
                pedestrian.gameObject.SetActive(true);
                pedestrianStore.Add(pedestrian);

                count++;
            }
        }
    }
}
