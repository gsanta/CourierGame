using UnityEngine;

namespace Movement
{
    public class GridCreator : MonoBehaviour
    {
        [SerializeField]
        private GameObject container;

        private void Start()
        {
            var center = container.transform.position;
            int rows = 6;
            int cols = 6;
            int size = 1;

            for (int i = -rows / 2; i < rows / 2; i++)
            {
                for (int j = -cols / 2; j < cols / 2; j++)
                {
                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Quad);

                    go.transform.SetParent(container.transform);
                    go.layer = LayerMask.NameToLayer("Route");

                    MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
                    meshRenderer.sharedMaterial = new Material(Shader.Find("Transparent/Diffuse"));
                    Color color = meshRenderer.sharedMaterial.color;
                    color.a = 0.5f;
                    meshRenderer.sharedMaterial.color = color;
                    meshRenderer.enabled = false;

                    MeshFilter meshFilter = go.AddComponent<MeshFilter>();

                    Mesh mesh = new Mesh();

                    Vector3[] vertices = new Vector3[4]
                    {
                        new Vector3(j, 1, i),
                        new Vector3(j + 1, 1, i),
                        new Vector3(j, 1, i + 1),
                        new Vector3(j + 1, 1, i + 1)
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
                    go.AddComponent<BoxCollider>();
                }
            }
        }
    }
}
