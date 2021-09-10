using UnityEngine;
using Zenject;

namespace Domain
{
    public class SteeringComponent : MonoBehaviour
    {
        private PedestrianStore pedestrianStore;
        private BikerStore bikerStore;

        public Steering Steering { get; private set; } 

        [Inject]
        public void Construct(PedestrianStore pedestrianStore, BikerStore bikerStore)
        {
            this.pedestrianStore = pedestrianStore;
            this.bikerStore = bikerStore;
            Steering = new Steering(this.gameObject);
        }

        private void Start()
        {
            Debug.Log("Started");
        }

        private void Update()
        {
            bikerStore.GetAll();


            var obstacle = FindMostRelevantObstacle();

            if (obstacle)
            {
                Debug.Log("obstacle found");
            }
        }

        private GameObject FindMostRelevantObstacle()
        {
            Biker closest = null;
            float closesDistance = float.MaxValue;

            foreach(Biker biker in bikerStore.GetAll())
            {
                bool intersects = Steering.Intersects(biker.GetComponent<SteeringComponent>().Steering);

                if (intersects)
                {
                    if (closest == null || Steering.Distance(biker.GetComponent<SteeringComponent>().Steering) < closesDistance)
                    {
                        closest = biker;
                    }
                }
            }

            if (closest != null)
            {
                return closest.gameObject;
            }

            return null;
        }

        public class Factory : PlaceholderFactory<UnityEngine.Object, SteeringComponent>
        {
        }
    }
}
