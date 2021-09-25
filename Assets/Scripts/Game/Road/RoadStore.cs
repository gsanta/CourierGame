using AI;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadStore : MonoBehaviour
    {
        [SerializeField]
        private GameObject waypointContainer;
        private List<Waypoint> waypoints = new List<Waypoint>();
        
        private void Awake()
        {
            foreach (Transform obj in waypointContainer.transform)
            {
                waypoints.Add(obj.GetComponent<Waypoint>());
            }
        }

        public List<Waypoint> Waypoints { get => waypoints; }
    }
}
