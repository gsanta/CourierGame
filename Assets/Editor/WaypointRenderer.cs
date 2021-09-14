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

            if (waypoint.previousWaypoint != null)
            {
                DrawRightBorder();

                if (waypoint.rightMargin != 0 || waypoint.previousWaypoint.rightMargin != 0)
                {
                    DrawRightMargin();
                }
            }

            if (waypoint.nextWaypoint != null)
            {
                DrawLeftBorder();

                if (waypoint.leftMargin != 0 || waypoint.nextWaypoint.leftMargin != 0)
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

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.previousWaypoint.transform.position + offsetTo);
        }

        private void DrawRightMargin()
        {
            Gizmos.color = LightRed;

            Waypoint previousWaypoint = waypoint.previousWaypoint;

            Vector3 offset = GetRightOffset() + waypoint.transform.right * waypoint.rightMargin;
            Vector3 offsetTo = GetRightOffsetTo() + previousWaypoint.transform.right * previousWaypoint.rightMargin;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.previousWaypoint.transform.position + offsetTo);
        }

        private Vector3 GetRightOffset()
        {
            return waypoint.transform.right * waypoint.width / 2f;
        }

        private Vector3 GetRightOffsetTo()
        {
            return waypoint.previousWaypoint.transform.right * waypoint.previousWaypoint.width / 2f;
        }

        private void DrawLeftBorder()
        {
            Gizmos.color = Color.green;
            Vector3 offset = -waypoint.transform.right * waypoint.width / 2f;
            Vector3 offsetTo = -waypoint.nextWaypoint.transform.right * waypoint.nextWaypoint.width / 2;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.nextWaypoint.transform.position + offsetTo);
        }

        private void DrawLeftMargin()
        {
            Gizmos.color = LightGreen;

            Waypoint nextWaypoint = waypoint.nextWaypoint;

            Vector3 offset = GetLeftOffset() - waypoint.transform.right * waypoint.leftMargin;
            Vector3 offsetTo = GetLeftOffsetTo(waypoint) - nextWaypoint.transform.right * nextWaypoint.leftMargin;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.nextWaypoint.transform.position + offsetTo);
        }

        private Vector3 GetLeftOffset()
        {
            return -waypoint.transform.right * waypoint.width / 2f;
        }

        private Vector3 GetLeftOffsetTo(Waypoint waypoint)
        {
            return -waypoint.nextWaypoint.transform.right * waypoint.nextWaypoint.width / 2f;
        }

        private void DrawBranches()
        {
            foreach (Waypoint branch in waypoint.branches)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(waypoint.transform.position, branch.transform.position);
            }
        }
    }
}
