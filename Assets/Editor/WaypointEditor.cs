using AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad()]
class WaypointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
    {
        if ((gizmoType & GizmoType.Selected) != null)
        {
            Gizmos.color = Color.yellow;
        } else
        {
            Gizmos.color = Color.yellow * 0.5f;
        }

        Gizmos.DrawSphere(waypoint.transform.position, .1f);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(
            waypoint.transform.position + (waypoint.transform.right * waypoint.width / 2f),
            waypoint.transform.position - (waypoint.transform.right * waypoint.width / 2f)
        );

        if (waypoint.previousWaypoint != null)
        {
            Gizmos.color = Color.red;
            Vector3 offset = waypoint.transform.right * waypoint.width / 2f;
            Vector3 offsetTo = waypoint.previousWaypoint.transform.right * waypoint.width / 2f;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.previousWaypoint.transform.position + offsetTo);
        }

        if (waypoint.nextWaypoint != null)
        {
            Gizmos.color = Color.green;
            Vector3 offset = - waypoint.transform.right * waypoint.width / 2f;
            Vector3 offsetTo = -waypoint.nextWaypoint.transform.right * waypoint.width / 2;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.nextWaypoint.transform.position + offsetTo);
        }
    }
}
