using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class WaypointRenderer
    {
        private readonly Waypoint waypoint;
        private readonly LineRenderer lineRenderer;
        private List<Vector3> points = new List<Vector3>();

        public WaypointRenderer(Waypoint waypoint, LineRenderer lineRenderer)
        {
            this.waypoint = waypoint;
            this.lineRenderer = lineRenderer;
        }

        public void Render()
        {
            List<Vector3> points = SetPoints();
            RenderLines(points);
        }

        public List<Vector3> GetPoints()
        {
            return points;
        }

        private void RenderLines(List<Vector3> points)
        {
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.positionCount = points.Count;
            lineRenderer.material.color = Color.blue;
            lineRenderer.SetPositions(points.ToArray());
            //lineRenderer.useWorldSpace = true;
        }

        private List<Vector3> SetPoints()
        {
            if (waypoint.PrevWayPoint != null)
            {
                (Vector3, Vector3) rightBorder = GetRightBorderPoints();
                points.Add(rightBorder.Item1);
                points.Add(rightBorder.Item2);
                (Vector3, Vector3) leftBorder = GetLeftBorderPoints();
                points.Add(leftBorder.Item1);
                points.Add(leftBorder.Item2);
            }
            else
            {
                points.Add(GetRightOffset());
                points.Add(GetLeftOffset());
            }

            //if (waypoint.NextWayPoint != null)
            //{
            //    (Vector3, Vector3) leftBorder = GetLeftBorderPoints();
            //    points.Add(leftBorder.Item1);
            //    points.Add(leftBorder.Item2);
            //}
            //else
            //{
            //    points.Add(GetLeftOffset());
            //}

            return points;
        }

        public (Vector3, Vector3) GetRightBorderPoints()
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

        public (Vector3, Vector3) GetLeftBorderPoints()
        {
            Gizmos.color = Color.green;
            Vector3 offset = GetLeftOffset();
            Vector3 offsetTo = GetLeftOffsetTo();

            return (offset, waypoint.PrevWayPoint.Position + offsetTo);
        }

        private Vector3 GetLeftOffset()
        {
            return (waypoint.transform.position  + -waypoint.transform.right * waypoint.width / 2f);
        }

        private Vector3 GetLeftOffsetTo()
        {
            return -waypoint.PrevWayPoint.Right * waypoint.PrevWayPoint.Width / 2f;
        }
    }
}
