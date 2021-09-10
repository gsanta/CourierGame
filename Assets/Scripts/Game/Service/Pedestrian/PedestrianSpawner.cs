using Domain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AI
{
    public class PedestrianSpawner : MonoBehaviour
    {
        public Pedestrian pedestrianPrefab;
        public int pedestriansToSpawn;

        private PedestrianStore pedestrianStore;

        [Inject]
        public void Construct(PedestrianStore pedestrianStore)
        {
            this.pedestrianStore = pedestrianStore;
        }

        void Start()
        {
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            int count = 0;

            while (count < pedestriansToSpawn)
            {
                Pedestrian obj = Instantiate(pedestrianPrefab);
                Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
                obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
                obj.transform.position = child.position;
                pedestrianStore.Add(obj);

                yield return new WaitForEndOfFrame();
                count++;
            }
        }
    }
}
