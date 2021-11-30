using UnityEngine;

namespace Movement
{
    public class Tile : MonoBehaviour
    {
        public static float width = 1.5f;
        public static float padding = 0.1f;
        public Vector3 CenterPoint { get; set; }

        public void SetCenterPoint(Vector3 centerPoint)
        {
            float halfWidth = width / 2;

            Vector3[] vertices = new Vector3[4]
            {
                new Vector3(centerPoint.x - halfWidth + padding, 1, centerPoint.z + halfWidth - padding),
                new Vector3(centerPoint.x + halfWidth - padding, 1, centerPoint.z + halfWidth - padding),
                new Vector3(centerPoint.x - halfWidth + padding, 1, centerPoint.z - halfWidth + padding),
                new Vector3(centerPoint.x + halfWidth - padding, 1, centerPoint.z - halfWidth + padding)
            };

            var mesh = GetComponent<MeshFilter>().mesh;
            mesh.vertices = vertices;

            //int[] tris = new int[6]
            //{
            //    // lower left triangle
            //    0, 1, 2,
            //    // upper right triangle
            //    2, 1, 3
            //};
            //mesh.triangles = tris;

            //Vector3[] normals = new Vector3[4]
            //{
            //    -Vector3.forward,
            //    -Vector3.forward,
            //    -Vector3.forward,
            //    -Vector3.forward
            //};
            //mesh.normals = normals;

            //Vector2[] uv = new Vector2[4]
            //{
            //    new Vector2(0, 0),
            //    new Vector2(1, 0),
            //    new Vector2(0, 1),
            //    new Vector2(1, 1)
            //};
            //mesh.uv = uv;
        }

        public void InitMesh()
        {
            float halfWidth = width / 2;

            Vector3[] vertices = new Vector3[4]
            {
                new Vector3(0 - halfWidth + padding, 1, 0 + halfWidth - padding),
                new Vector3(0 + halfWidth - padding, 1, 0 + halfWidth - padding),
                new Vector3(0 - halfWidth + padding, 1, 0 - halfWidth + padding),
                new Vector3(0 + halfWidth - padding, 1, 0 - halfWidth + padding)
            };

            gameObject.layer = LayerMask.NameToLayer("Route");

            MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = new Material(Shader.Find("Transparent/Diffuse"));
            meshRenderer.sharedMaterial.color = Color.grey;
            Color color = meshRenderer.sharedMaterial.color;
            color.a = 0.5f;
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
            gameObject.AddComponent<BoxCollider>();
        }
    }
}
