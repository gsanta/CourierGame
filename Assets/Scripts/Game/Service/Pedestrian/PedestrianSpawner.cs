using Domain;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Service
{
    public class PedestrianSpawner : MonoBehaviour
    {
        public int pedestriansToSpawn;

        private PedestrianStore pedestrianStore;
        private PedestrianFactory pedestrianFactory;

        [Inject]
        public void Construct(PedestrianStore pedestrianStore, PedestrianFactory pedestrianFactory)
        {
            this.pedestrianStore = pedestrianStore;
            this.pedestrianFactory = pedestrianFactory;
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

                var transf = transform.GetChild(Random.Range(0, transform.childCount - 1));

                var pedestrian = pedestrianFactory.Create(new PedestrianConfig(transf.gameObject));
                pedestrianStore.Add(pedestrian);

                yield return new WaitForEndOfFrame();
                count++;
            }
        }
    }
}
