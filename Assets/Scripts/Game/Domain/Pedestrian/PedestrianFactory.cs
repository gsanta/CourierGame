using Service.AI;
using UnityEngine;
using Zenject;

namespace Domain
{
    public class PedestrianFactory : MonoBehaviour, ItemFactory<PedestrianConfig, Pedestrian>
    {
        [SerializeField]
        private Pedestrian pedestrianPrefab;
        [SerializeField]
        private GameObject pedestrianContainer;

        private PedestrianStore pedestrianStore;
        private BikerStore bikerStore;

        [Inject]
        public void Construct(PedestrianStore pedestrianStore, BikerStore bikerStore)
        {
            this.pedestrianStore = pedestrianStore;
            this.bikerStore = bikerStore;
        }

        private void Start()
        {
            var childCount = pedestrianContainer.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                var pedestrian = pedestrianContainer.transform.GetChild(i).GetComponent<Pedestrian>();
                InitializeObj(pedestrian);
            }
        }

        public Pedestrian Create(PedestrianConfig config)
        {
            Pedestrian pedestrian = Instantiate(pedestrianPrefab, pedestrianContainer.transform);
            Initialize(pedestrian);
            Transform child = config.spawnPoint.transform;
            pedestrian.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            pedestrian.transform.position = child.position;

            return pedestrian;
        }

        private void InitializeObj(Pedestrian pedestrian)
        {
            Initialize(pedestrian);
            pedestrian.transform.position = pedestrian.GetComponent<WaypointNavigator>().currentWaypoint.transform.position;
        }

        private void Initialize(Pedestrian pedestrian)
        {
            pedestrian.GetComponent<SteeringComponent>().Construct(pedestrianStore, bikerStore);
        }
    }
}
