using Domain;
using Service.AI;
using UnityEditor;
using UnityEngine;

//namespace Editor
//{
[InitializeOnLoad()]
class SteeringVisualizer
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(Pedestrian waypoint, GizmoType gizmoType)
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawLine(
        //    pedestrian.transform.position,
        //    pedestrian.transform.position + pedestrian.transform.forward * pedestrian.seeAhead
        //);

        var color = new Color(1, 0.8f, 0.4f, 0.2f);
        Handles.color = color;
        //Handles.DrawSolidDisc(pedestrian.transform.position, pedestrian.transform.up, 3f);
    }
}
//}
