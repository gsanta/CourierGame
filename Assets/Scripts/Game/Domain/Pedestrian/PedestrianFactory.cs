using Service.AI;
using UnityEngine;
using Zenject;

namespace Domain
{
    public class PedestrianFactory : MonoBehaviour, ItemFactory<PedestrianConfig, Pedestrian>
    {
        [SerializeField]
        private Pedestrian pedestrianPrefab;

        private Pedestrian.Factory instanceFactory;

        [Inject]
        public void Construct(Pedestrian.Factory instanceFactory)
        {
            this.instanceFactory = instanceFactory;
        }

        public Pedestrian Create(PedestrianConfig config)
        {
            Pedestrian pedestrian = instanceFactory.Create(pedestrianPrefab);
            Transform child = config.spawnPoint.transform;
            pedestrian.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            pedestrian.transform.position = child.position;

            return pedestrian;
        }
    }
}
