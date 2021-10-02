using AI;
using UnityEngine;

namespace Pedestrians
{
    public class PedestrianFactory : MonoBehaviour, ItemFactory<PedestrianConfig, Pedestrian>
    {
        [SerializeField]
        private Pedestrian pedestrianPrefab;
        [SerializeField]
        private GameObject pedestrianContainer;

        public GameObject PedestrianContainer { get => pedestrianContainer; }

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
