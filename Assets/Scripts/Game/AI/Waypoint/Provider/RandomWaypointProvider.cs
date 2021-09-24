using UnityEngine;

namespace AI
{
    class RandomWaypointProvider : MonoBehaviour, IWaypointProvider
    {

        public bool IsRecordingProvider()
        {
            return false;
        }

        public WaypointInfo GetNextWaypoint()
        {
            WaypointNavigator navigator = GetComponent<WaypointNavigator>();
            Waypoint currentWaypoint = navigator.currentWaypoint;
            int direction = navigator.direction;

            bool shouldBranch = ShouldBranch(currentWaypoint);

            if (shouldBranch)
            {
                return GetBranchWaypoint(currentWaypoint, direction);
            }
            else if (direction == 0)
            {
                return GetNextWaypoint(currentWaypoint, direction);
            }
            else
            {
                return GetPrevWaypoint(currentWaypoint, direction);
            }
        }

        private WaypointInfo GetBranchWaypoint(Waypoint currentWaypoint, int direction)
        {
            Waypoint newWaypoint = currentWaypoint.branches[UnityEngine.Random.Range(0, currentWaypoint.branches.Count - 1)];

            return new WaypointInfo(newWaypoint, direction);
        }

        private WaypointInfo GetNextWaypoint(Waypoint currentWaypoint, int direction)
        {
            Waypoint newWaypoint;
            int newDirection = direction;

            if (currentWaypoint.nextWaypoint != null)
            {
                newWaypoint = currentWaypoint.nextWaypoint;
            }
            else
            {
                newWaypoint = currentWaypoint.previousWaypoint;
                newDirection = 1;
            }

            return new WaypointInfo(newWaypoint, newDirection);
        }

        private WaypointInfo GetPrevWaypoint(Waypoint currentWaypoint, int direction)
        {
            Waypoint newWaypoint;
            int newDirection = direction;

            if (currentWaypoint.previousWaypoint != null)
            {
                newWaypoint = currentWaypoint.previousWaypoint;
            }
            else
            {
                newWaypoint = currentWaypoint.nextWaypoint;
                newDirection = 0;
            }

            return new WaypointInfo(newWaypoint, newDirection);
        }

        private bool ShouldBranch(Waypoint currentWaypoint)
        {
            if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
            {
                return UnityEngine.Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
            }

            return false;
        }
    }
}
