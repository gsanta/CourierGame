
using AI;
using Route;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class RoadStoreController : MonoBehaviour
    {
        [SerializeField]
        private GameObject waypointContainer;
        private RoadStore roadStore;

        [Inject]
        public void Construct(RoadStore roadStore)
        {
            this.roadStore = roadStore;
        }

        private void Awake()
        {
            List<Waypoint> waypoints = new List<Waypoint>();
            foreach (Transform obj in waypointContainer.transform)
            {
                waypoints.Add(obj.GetComponent<Waypoint>());
            }

            roadStore.SetWaypoints(waypoints);
        }
    }
}
