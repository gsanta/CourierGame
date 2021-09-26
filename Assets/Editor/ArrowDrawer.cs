
using UnityEngine;

namespace Editor
{
    public static class ArrowDrawer
    {
        public static void ForGizmo(Vector3 pos, Vector3 end, float arrowHeadLength = 0.5f, float arrowHeadAngle = 20.0f)
        {
            var direction = end - pos;
            Gizmos.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(pos + direction / 2, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction / 2, left * arrowHeadLength);
        }
    }
}
