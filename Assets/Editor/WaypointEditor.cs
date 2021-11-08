using Editor;
using AI;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad()]
class WaypointEditor
{
    private static WaypointRenderer waypointRenderer = new WaypointRenderer();

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
    {
        waypointRenderer.RenderGizmo(waypoint, gizmoType);
    }
}
