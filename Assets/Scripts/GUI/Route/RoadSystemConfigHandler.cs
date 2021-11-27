using AI;
using Controls;
using Pedestrians;
using Route;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class RoadSystemConfigHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject roadWaypointContainer;
        [SerializeField]
        private GameObject pavementWaypointContainer;
        [SerializeField]
        private GameObject walkTargets;

        private StoreSetup storeSetup;
        private RoadStore roadStore;
        private WalkTargetStore walkTargetStore;

        [Inject]
        public void Construct(StoreSetup storeSetup, RoadStore roadStore, WalkTargetStore walkTargetStore)
        {
            this.storeSetup = storeSetup;
            this.roadStore = roadStore;
            this.walkTargetStore = walkTargetStore;
        }

        private void Awake()
        {
            //List<Waypoint> waypoints = new List<Waypoint>();
            //foreach (Transform obj in pavementWaypointContainer.transform)
            //{
            //    waypoints.Add(obj.GetComponent<Waypoint>());
            //}

            //roadStore.SetWaypoints(waypoints);

            storeSetup.SetupStore(walkTargets, walkTargetStore);
        }
    }
}
