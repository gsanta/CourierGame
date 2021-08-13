using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI
{
    class RecordedWaypointProvider : MonoBehaviour, IWaypointProvider
    {

        List<WaypointInfo> waypointSnapshots = new List<WaypointInfo>();
        private int currentIndex;

        public WaypointInfo GetNextWaypoint()
        {

        }

        public bool IsRecordingProvider()
        {
            return true;
        }

        public void StartPlayBack(int time)
        {

        }
    }
}
