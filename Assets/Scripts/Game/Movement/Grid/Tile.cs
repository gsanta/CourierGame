using UnityEngine;

namespace Movement
{
    public class Tile : MonoBehaviour
    {
        public static float width = 1.5f;
        public static float padding = 0.1f;
        public Vector3 CenterPoint { get; set; }

        private Vector3 centerPoint;

        private static float UNHOVER_OPACITY = 0.3f;
        private static float HOVER_OPACITY = 0.8f;

        public void SetCenterPoint(Vector3 point)
        {
            centerPoint = point;
            float halfWidth = width / 2;

            Vector3[] vertices = new Vector3[4]
            {
                new Vector3(centerPoint.x - halfWidth + padding, 0.1f, centerPoint.z + halfWidth - padding),
                new Vector3(centerPoint.x + halfWidth - padding, 0.1f, centerPoint.z + halfWidth - padding),
                new Vector3(centerPoint.x - halfWidth + padding, 0.1f, centerPoint.z - halfWidth + padding),
                new Vector3(centerPoint.x + halfWidth - padding, 0.1f, centerPoint.z - halfWidth + padding)
            };

            var mesh = GetComponent<MeshFilter>().mesh;
            mesh.vertices = vertices;
            mesh.RecalculateBounds();

            GetComponent<MeshCollider>().sharedMesh = mesh;
        }

        public Vector3 GetCenterPoint()
        {
            return centerPoint;
        }

        public void InitMesh()
        {
            float halfWidth = width / 2;

            Vector3[] vertices = new Vector3[4]
            {
                new Vector3(0 - halfWidth + padding, 0.1f, 0 + halfWidth - padding),
                new Vector3(0 + halfWidth - padding, 0.1f, 0 + halfWidth - padding),
                new Vector3(0 - halfWidth + padding, 0.1f, 0 - halfWidth + padding),
                new Vector3(0 + halfWidth - padding, 0.1f, 0 - halfWidth + padding)
            };

            gameObject.layer = LayerMask.NameToLayer("Route");

            MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = new Material(Shader.Find("Transparent/Diffuse"));
            meshRenderer.sharedMaterial.color = Color.blue;
            Color color = meshRenderer.sharedMaterial.color;
            color.a = UNHOVER_OPACITY;
            meshRenderer.sharedMaterial.color = color;

            meshRenderer.enabled = true;

            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

            Mesh mesh = new Mesh();

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
            gameObject.AddComponent<MeshCollider>();
        }

        public void Hover()
        {
            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            Color color = meshRenderer.sharedMaterial.color;
            color.a = HOVER_OPACITY;
            meshRenderer.sharedMaterial.color = color;
        }

        public void UnHover()
        {
            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            Color color = meshRenderer.sharedMaterial.color;
            color.a = UNHOVER_OPACITY;
            meshRenderer.sharedMaterial.color = color;
        }

        public void SetVisible(bool visible)
        {
            GetComponent<MeshRenderer>().enabled = visible;
        }

        public bool IsVisible()
        {
            return GetComponent<MeshRenderer>().enabled;
        }
    }
}
