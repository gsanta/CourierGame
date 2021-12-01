using UnityEngine;

namespace AI
{
    public class WaypointQuad : MonoBehaviour
    {
        public Vector3 CenterPoint { get; set; }
        private Waypoint waypoint;

        public Waypoint GetWaypoint()
        {
            return waypoint;
        }

        public void Setup(Waypoint waypoint, GameObject quadContainer)
        {
            this.waypoint = waypoint;
            var points = waypoint.waypointRenderer.GetPoints();

            if (points.Count == 4)
            {
                CenterPoint = (points[0] + points[2]) / 2;

                gameObject.transform.SetParent(quadContainer.transform);
                gameObject.layer = LayerMask.NameToLayer("Route");

                MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
                meshRenderer.sharedMaterial = new Material(Shader.Find("Transparent/Diffuse"));
                Color color = meshRenderer.sharedMaterial.color;
                color.a = 0.5f;
                meshRenderer.sharedMaterial.color = color;
                meshRenderer.enabled = false;

                MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();

                Vector3[] vertices = new Vector3[4]
                {
                    points[1],
                    points[0],
                    points[2],
                    points[3]
                };
                mesh.vertices = vertices;

                int[] tris = new int[6]
                {
                    // lower left triangle
                    0, 1, 2,
                    // upper right triangle
                    2, 1, 3
                };
                mesh.triangles = tris;

                Vector3[] normals = new Vector3[4]
                {
                    -Vector3.forward,
                    -Vector3.forward,
                    -Vector3.forward,
                    -Vector3.forward
                };
                mesh.normals = normals;

                Vector2[] uv = new Vector2[4]
                {
                    new Vector2(0, 0),
                    new Vector2(1, 0),
                    new Vector2(0, 1),
                    new Vector2(1, 1)
                };
                mesh.uv = uv;

                meshFilter.mesh = mesh;
                gameObject.AddComponent<BoxCollider>();
            }
        }
    }
}
