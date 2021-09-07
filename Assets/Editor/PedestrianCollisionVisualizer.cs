using Domain;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad()]
    public class PedestrianCollisionVisualizer
    {
        [DrawGizmo(GizmoType.Selected)]
        public static void OnDrawSceneGizmo(Pedestrian pedestrian, GizmoType gizmoType)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(
                pedestrian.transform.position,
                pedestrian.transform.position + pedestrian.transform.forward * pedestrian.seeAhead
            );

            var color = new Color(1, 0.8f, 0.4f, 0.2f);
            Handles.color = color;
            Handles.DrawSolidDisc(pedestrian.transform.position, pedestrian.transform.up, 3f);
        }
    }
}
