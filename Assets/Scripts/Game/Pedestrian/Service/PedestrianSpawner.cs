using Route;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Pedestrians
{
    public class PedestrianSpawner : MonoBehaviour
    {
        public int pedestriansToSpawn;

        private PedestrianStore pedestrianStore;
        private PedestrianFactory pedestrianFactory;
        private RoadStore roadStore;

        [Inject]
        public void Construct(PedestrianStore pedestrianStore, PedestrianFactory pedestrianFactory, RoadStore roadStore)
        {
            this.pedestrianStore = pedestrianStore;
            this.pedestrianFactory = pedestrianFactory;
            this.roadStore = roadStore;
        }

        void Start()
        {
            //InitPreExistingObjects();
            StartCoroutine(Spawn());
        }

        private void InitPreExistingObjects()
        {
            var pedestrianContainer = pedestrianFactory.PedestrianContainer;
            var childCount = pedestrianContainer.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                var pedestrian = pedestrianContainer.transform.GetChild(i).GetComponent<Pedestrian>();
                pedestrianFactory.InitializeObj(pedestrian);
                pedestrianStore.Add(pedestrian);
            }
        }

        IEnumerator Spawn()
        {
            int count = 0;

            while (count < pedestriansToSpawn)
            {

                //var waypoint = roadStore.Pavements[Random.Range(0, roadStore.Pavements.Count - 1)];

                //var pedestrian = pedestrianFactory.Create(new PedestrianConfig(waypoint.gameObject));
                //pedestrianStore.Add(pedestrian);

                yield return new WaitForEndOfFrame();
                count++;
            }
        }
    }
}
