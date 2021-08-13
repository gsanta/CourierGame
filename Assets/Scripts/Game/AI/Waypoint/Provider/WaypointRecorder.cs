using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI
{
    public class WaypointRecorder : MonoBehaviour
    {
        private WaypointNavigator waypointNavigator;

        void Start()
        {
            waypointNavigator = GetComponent<WaypointNavigator>();
        }

    }
}
