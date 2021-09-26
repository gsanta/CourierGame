using AI;
using Bikers;
using Model;
using UnityEngine;
using Zenject;

namespace Pedestrians
{
    public class PedestrianFactory : MonoBehaviour, ItemFactory<PedestrianConfig, Pedestrian>
    {
        [SerializeField]
        private Pedestrian pedestrianPrefab;
        [SerializeField]
        private GameObject pedestrianContainer;

        private PedestrianStore pedestrianStore;
        private BikerStore bikerStore;
        private Timer timer;

        public GameObject PedestrianContainer { get => pedestrianContainer; }

        [Inject]
        public void Construct(PedestrianStore pedestrianStore, BikerStore bikerStore, Timer timer)
        {
            this.pedestrianStore = pedestrianStore;
            this.bikerStore = bikerStore;
            this.timer = timer;
        }

        public Pedestrian Create(PedestrianConfig config)
        {
            Pedestrian pedestrian = Instantiate(pedestrianPrefab, pedestrianContainer.transform);
            Transform child = config.spawnPoint.transform;
            pedestrian.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            pedestrian.transform.position = child.position;

            return pedestrian;
        }

        public void InitializeObj(Pedestrian pedestrian)
        {
            pedestrian.transform.position = pedestrian.GetComponent<WaypointNavigator>().currentWaypoint.transform.position;
        }
    }
}
