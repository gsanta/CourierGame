using AI;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class WaypointRenderer
    {
        private static Color LightGreen = new Color(0.611f, 0.878f, 0.529f);
        private static Color LightRed = new Color(0.968f, 0.494f, 0.494f);

        private Waypoint waypoint;
        private GizmoType gizmoType;

        public void RenderGizmo(Waypoint waypoint, GizmoType gizmoType)
        {
            this.waypoint = waypoint;
            this.gizmoType = gizmoType;

            DrawCenterPoint();
            DrawLine();

            if (waypoint.PrevWayPoint != null)
            {
                DrawRightBorder();

                if (waypoint.rightMargin != 0 || waypoint.PrevWayPoint.RightMargin != 0)
                {
                    DrawRightMargin();
                }
            }

            if (waypoint.NextWayPoint != null)
            {
                DrawLeftBorder();
                DrawArrow();

                if (waypoint.leftMargin != 0 || waypoint.NextWayPoint.LeftMargin != 0)
                {
                    DrawLeftMargin();
                }
            }

            if (waypoint.branches != null)
            {
                DrawBranches();
            }
        }

        private void DrawCenterPoint()
        {
            if ((gizmoType & GizmoType.Selected) != 0)
            {
                Gizmos.color = Color.yellow;
            }
            else
            {
                Gizmos.color = Color.yellow * 0.5f;
            }

            Gizmos.DrawSphere(waypoint.transform.position, .1f);
        }

        private void DrawLine()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(
                waypoint.transform.position + GetRightOffset() + GetRightMarginOffset(),
                waypoint.transform.position + GetLeftOffset() + GetLeftMarginOffset()
            );
        }

        private Vector3 GetRightMarginOffset()
        {
            return waypoint.transform.right * waypoint.rightMargin;
        }

        private Vector3 GetLeftMarginOffset()
        {
            return -waypoint.transform.right * waypoint.leftMargin;
        }

        private void DrawRightBorder()
        {
            Gizmos.color = Color.red;
            Vector3 offset = GetRightOffset();
            Vector3 offsetTo = GetRightOffsetTo();

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.PrevWayPoint.Position + offsetTo);
        }

        private void DrawRightMargin()
        {
            Gizmos.color = LightRed;

            Waypoint previousWaypoint = waypoint.PrevWayPoint;

            Vector3 offset = GetRightOffset() + waypoint.transform.right * waypoint.rightMargin;
            Vector3 offsetTo = GetRightOffsetTo() + previousWaypoint.Right * previousWaypoint.RightMargin;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.PrevWayPoint.Position + offsetTo);
        }

        private Vector3 GetRightOffset()
        {
            return waypoint.transform.right * waypoint.width / 2f;
        }

        private Vector3 GetRightOffsetTo()
        {
            return waypoint.PrevWayPoint.Right * waypoint.PrevWayPoint.Width / 2f;
        }

        private void DrawLeftBorder()
        {
            Gizmos.color = Color.green;
            Vector3 offset = -waypoint.transform.right * waypoint.width / 2f;
            Vector3 offsetTo = -waypoint.NextWayPoint.Right * waypoint.NextWayPoint.Width / 2;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.NextWayPoint.Position + offsetTo);
        }

        private void DrawLeftMargin()
        {
            Gizmos.color = LightGreen;

            Waypoint nextWaypoint = waypoint.NextWayPoint;

            Vector3 offset = GetLeftOffset() - waypoint.transform.right * waypoint.leftMargin;
            Vector3 offsetTo = GetLeftOffsetTo(waypoint) - nextWaypoint.Right * nextWaypoint.LeftMargin;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.NextWayPoint.Position + offsetTo);
        }

        private Vector3 GetLeftOffset()
        {
            return -waypoint.transform.right * waypoint.width / 2f;
        }

        private Vector3 GetLeftOffsetTo(Waypoint waypoint)
        {
            return -waypoint.NextWayPoint.Right * waypoint.NextWayPoint.Width / 2f;
        }

        private void DrawBranches()
        {
            foreach (Waypoint branch in waypoint.branches)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(waypoint.transform.position, branch.transform.position);
            }
        }

        private void DrawArrow()
        {
            Gizmos.color = Color.blue;

            Waypoint nextWaypoint = waypoint.NextWayPoint;

            if (waypoint.direction == 1)
            {
                ArrowDrawer.ForGizmo(waypoint.transform.position, nextWaypoint.transform.position);
            } else if (waypoint.direction == -1)
            {
                ArrowDrawer.ForGizmo(nextWaypoint.transform.position, waypoint.transform.position);
            } else
            {
                Gizmos.DrawLine(waypoint.transform.position, nextWaypoint.transform.position);
            }
        }
    }
}

