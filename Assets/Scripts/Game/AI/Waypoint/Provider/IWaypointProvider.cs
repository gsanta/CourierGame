using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{

    public struct WaypointInfo
    {
        public Waypoint waypoint;
        public int direction;

        public WaypointInfo(Waypoint waypoint, int direction)
        {
            this.waypoint = waypoint;
            this.direction = direction;
        }
    }

    public interface IWaypointProvider
    {
        WaypointInfo GetNextWaypoint();
        bool IsRecordingProvider();
    }
}
