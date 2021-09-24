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
            IWaypoint newWaypoint = currentWaypoint.Branches[UnityEngine.Random.Range(0, currentWaypoint.branches.Count - 1)];

            return new WaypointInfo((Waypoint)newWaypoint, direction);
        }

        private WaypointInfo GetNextWaypoint(IWaypoint currentWaypoint, int direction)
        {
            IWaypoint newWaypoint;
            int newDirection = direction;

            if (currentWaypoint.NextWayPoint != null)
            {
                newWaypoint = currentWaypoint.NextWayPoint;
            }
            else
            {
                newWaypoint = currentWaypoint.PrevWayPoint;
                newDirection = 1;
            }

            return new WaypointInfo((Waypoint)newWaypoint, newDirection);
        }

        private WaypointInfo GetPrevWaypoint(IWaypoint currentWaypoint, int direction)
        {
            IWaypoint newWaypoint;
            int newDirection = direction;

            if (currentWaypoint.PrevWayPoint != null)
            {
                newWaypoint = currentWaypoint.PrevWayPoint;
            }
            else
            {
                newWaypoint = currentWaypoint.NextWayPoint;
                newDirection = 0;
            }

            return new WaypointInfo((Waypoint)newWaypoint, newDirection);
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
