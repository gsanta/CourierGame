using AI;
using Route;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class PavementStoreController : MonoBehaviour
    {
        [SerializeField]
        private GameObject WaypointContainer;
        private PavementStore pavementStore;

        [Inject]
        public void Construct(PavementStore pavementStore)
        {
            this.pavementStore = pavementStore;
        }

        private void Awake()
        {
            List<Waypoint> waypoints = new List<Waypoint>();
            foreach (Transform obj in WaypointContainer.transform)
            {
                waypoints.Add(obj.GetComponent<Waypoint>());
            }

            pavementStore.SetWaypoints(waypoints);
        }
    }
}
