using UnityEngine;

namespace AI
{
    public class WaypointScorer : Scorer<Waypoint>
    {
        public float computeCost(Waypoint from, Waypoint to)
        {
            return Vector3.Distance(from.transform.position, to.transform.position);
        }
    }
}
