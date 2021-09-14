using Model;
using UnityEditor;
using UnityEngine;

//namespace Editor
//{
[InitializeOnLoad()]
class SteeringVisualizer
{
    [DrawGizmo(GizmoType.Selected)]
    public static void OnDrawSceneGizmo(SteeringComponent steeringComponent, GizmoType gizmoType)
    {
        GameObject gameObject = steeringComponent.gameObject;
        Steering steering = steeringComponent.Steering;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            gameObject.transform.position,
            gameObject.transform.position + gameObject.transform.forward * steering.seeAhead
        );

        var color = new Color(1, 0.8f, 0.4f, 0.2f);
        Handles.color = color;
        Handles.DrawSolidDisc(gameObject.transform.position, gameObject.transform.up, steering.radius);
    }
}
//}
