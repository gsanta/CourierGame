using Route;
using UnityEngine;

namespace Pedestrians
{
    public class PedestrianSpawner
    {
        private int pedestrianCount;

        private PedestrianStore pedestrianStore;
        private PedestrianFactory pedestrianFactory;
        private PavementStore pavementStore;

        public PedestrianSpawner(PedestrianStore pedestrianStore, PedestrianFactory pedestrianFactory, PavementStore pavementStore)
        {
            this.pedestrianStore = pedestrianStore;
            this.pedestrianFactory = pedestrianFactory;
            this.pavementStore = pavementStore;
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

                var waypoint = pavementStore.GetWaypoints()[Random.Range(0, pavementStore.GetWaypoints().Count - 1)];

                var pedestrian = pedestrianFactory.Create(new PedestrianConfig(waypoint.gameObject));
                pedestrian.gameObject.SetActive(true);
                pedestrianStore.Add(pedestrian);

                count++;
            }
        }
    }
}
