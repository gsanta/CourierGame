using AI;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class RoadConfig : MonoBehaviour
    {
        [SerializeField]
        public GameObject quadContainer;
        [SerializeField]
        public WaypointQuad quadTemplate;
        [SerializeField]
        private GameObject waypointContainer;

        public List<Waypoint> Waypoints { get; private set; }


        private void Awake()
        {
            List<Waypoint> waypoints = new List<Waypoint>();
            foreach (Transform obj in waypointContainer.transform)
            {
                waypoints.Add(obj.GetComponent<Waypoint>());
            }

            Waypoints = waypoints;
        }
    }
}
