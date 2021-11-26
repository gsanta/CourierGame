using Cameras;
using UnityEngine;
using Zenject;

namespace Movement
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField]
        private int width;
        [SerializeField]
        private int height;
        [SerializeField]
        private GameObject lightTile;
        [SerializeField]
        private GameObject darkTile;
        [SerializeField]
        private int tileSize;

        private GridStore gridStore;
        private CameraController cameraController;

        [Inject]
        public void Construct(GridStore gridStore, CameraController cameraController)
        {
            this.gridStore = gridStore;
            this.cameraController = cameraController;
        }

        private void Awake()
        {
            //cameraController.PanTo(gameObject);


            int startX = -Mathf.FloorToInt(width / 2f);
            int startZ = -Mathf.FloorToInt(height / 2f);
            //float start = Mathf.Floor(width / 2f);
            int counter = 0;

            for (int x = startX; x < width; x++)
            {
                for (int z = startZ; z < height; z++)
                {
                    
                    //CreateQuad(x * tileSize, z * tileSize, (z * width + x) % 2 == 0);
                    CreateQuad(x, z, counter % 2 == 0);

                    counter++;
                }
            }
        }

        private void CreateQuad(int x, int z, bool dark)
        {
            Vector3 parentPos = transform.position;
            GameObject tile = dark ? darkTile : lightTile;
            float y = tile.transform.position.y;
            var gameObject = Instantiate(dark ? darkTile : lightTile, transform);
            gameObject.transform.position = new Vector3(x * tile.transform.localScale.x + parentPos.x, y, z * tile.transform.localScale.z + parentPos.z);
            gameObject.SetActive(true);
            //gameObject.GetComponent<Renderer>().material.color = dark ? Color.red : Color.green;
        }

        //        private void CreateQuad(int x, int z, bool dark)
        //        {
        //            var gameObject = new GameObject();
        //            gameObject.transform.SetParent(transform);

        //            MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        //            meshRenderer.sharedMaterial = new Material(Shader.Find("Transparent/Diffuse"));
        //            meshRenderer.sharedMaterial.color = dark ? Color.red : Color.green;
        //            //Color color = meshRenderer.sharedMaterial.color;
        //            //color.a = 0.5f;
        //            //meshRenderer.sharedMaterial.color = color;
        //            //meshRenderer.enabled = false;

        //            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        //            Mesh mesh = new Mesh();

        //            Vector3[] vertices = new Vector3[4]
        //            {
        //                new Vector3(x, y, z),
        //                new Vector3(x + tileSize, y, z),
        //                new Vector3(x + tileSize, y, z + tileSize),
        //                new Vector3(x, y, z + tileSize)
        //            };
        //            mesh.vertices = vertices;

        //            int[] tris = new int[6]
        //{
        //                    // lower left triangle
        //                    0, 1, 2,
        //                    // upper right triangle
        //                    2, 1, 3
        //};
        //            mesh.triangles = tris;

        //            Vector3[] normals = new Vector3[4]
        //            {
        //                    -Vector3.forward,
        //                    -Vector3.forward,
        //                    -Vector3.forward,
        //                    -Vector3.forward
        //            };
        //            mesh.normals = normals;

        //            Vector2[] uv = new Vector2[4]
        //            {
        //                    new Vector2(0, 0),
        //                    new Vector2(1, 0),
        //                    new Vector2(0, 1),
        //                    new Vector2(1, 1)
        //            };
        //            mesh.uv = uv;

        //            meshFilter.mesh = mesh;
        //            gameObject.AddComponent<BoxCollider>();
        //        }
    }
}
