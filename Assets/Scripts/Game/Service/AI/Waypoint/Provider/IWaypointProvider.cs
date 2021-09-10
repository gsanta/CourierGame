namespace Service.AI
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
