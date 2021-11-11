using Editor;
using AI;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad()]
class WaypointEditor
{
    private static EditorWaypointRenderer waypointRenderer = new EditorWaypointRenderer();

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
    {
        waypointRenderer.RenderGizmo(waypoint, gizmoType);
    }
}
