using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class WaypointRenderer
    {
        private readonly Waypoint waypoint;
        private readonly LineRenderer lineRenderer;

        public WaypointRenderer(Waypoint waypoint, LineRenderer lineRenderer)
        {
            this.waypoint = waypoint;
            this.lineRenderer = lineRenderer;
        }

        public void Render()
        {
            List<Vector3> points = GetPoints();
            RenderLines(points);
        }

        private void RenderLines(List<Vector3> points)
        {
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
            lineRenderer.useWorldSpace = true;
        }

        private List<Vector3> GetPoints()
        {
            List<Vector3> points = new List<Vector3>();

            if (waypoint.PrevWayPoint != null)
            {
                (Vector3, Vector3) rightBorder = GetRightBorderPoints();
                points.Add(rightBorder.Item1);
                points.Add(rightBorder.Item2);
            }
            else
            {
                points.Add(GetRightOffset());
            }

            if (waypoint.NextWayPoint != null)
            {
                (Vector3, Vector3) leftBorder = GetLeftBorderPoints();
                points.Add(leftBorder.Item1);
                points.Add(leftBorder.Item2);
            }
            else
            {
                points.Add(GetLeftOffset());
            }

            return points;
        }

        private (Vector3, Vector3) GetRightBorderPoints()
        {
            Gizmos.color = Color.red;
            Vector3 offset = GetRightOffset();
            Vector3 offsetTo = GetRightOffsetTo();

            return (waypoint.PrevWayPoint.Position + offsetTo, offset);
        }

        private Vector3 GetRightOffset()
        {
            return waypoint.transform.position + waypoint.transform.right * waypoint.width / 2f;
        }

        private Vector3 GetRightOffsetTo()
        {
            return waypoint.PrevWayPoint.Right * waypoint.PrevWayPoint.Width / 2f;
        }

        private (Vector3, Vector3) GetLeftBorderPoints()
        {
            Gizmos.color = Color.green;
            Vector3 offset = GetLeftOffset();
            Vector3 offsetTo = GetLeftOffsetTo();

            return (offset, waypoint.NextWayPoint.Position + offsetTo);
        }

        private Vector3 GetLeftOffset()
        {
            return (waypoint.transform.position  + -waypoint.transform.right * waypoint.width / 2f);
        }

        private Vector3 GetLeftOffsetTo()
        {
            return -waypoint.NextWayPoint.Right * waypoint.NextWayPoint.Width / 2;
        }

        private void DrawLine()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(
                GetRightOffset(),
                GetLeftOffset()
            );
        }
    }
}
