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
                meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
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

        public void SetAllowedColor()
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<MeshRenderer>().material.color = Color.green;
        }

        public void SetNotAllowedColor()
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<MeshRenderer>().material.color = Color.red;
        }

        public void Hide()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        //public WaypointQuad(Waypoint waypoint, GameObject quadContainer)
        //{
        //    this.waypoint = waypoint;
        //    this.quadContainer = quadContainer;
        //}

        //public void Start()
        //{
        //    var points = waypoint.waypointRenderer.GetPoints();

        //    if (points.Count == 4)
        //    {
        //        var quadGameObject = new GameObject();
        //        quadGameObject.transform.SetParent(quadContainer.transform);
        //        quadGameObject.layer = LayerMask.NameToLayer("Route");

        //        MeshRenderer meshRenderer = quadGameObject.AddComponent<MeshRenderer>();
        //        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

        //        MeshFilter meshFilter = quadGameObject.AddComponent<MeshFilter>();

        //        Mesh mesh = new Mesh();

        //        Vector3[] vertices = new Vector3[4]
        //        {
        //            points[1],
        //            points[0],
        //            points[2],
        //            points[3]
        //        };
        //        mesh.vertices = vertices;

        //        int[] tris = new int[6]
        //        {
        //            // lower left triangle
        //            0, 2, 1,
        //            // upper right triangle
        //            2, 3, 1
        //        };
        //        mesh.triangles = tris;

        //        Vector3[] normals = new Vector3[4]
        //        {
        //            -Vector3.forward,
        //            -Vector3.forward,
        //            -Vector3.forward,
        //            -Vector3.forward
        //        };
        //        mesh.normals = normals;

        //        Vector2[] uv = new Vector2[4]
        //        {
        //            new Vector2(0, 0),
        //            new Vector2(1, 0),
        //            new Vector2(0, 1),
        //            new Vector2(1, 1)
        //        };
        //        mesh.uv = uv;

        //        meshFilter.mesh = mesh;
        //        quadGameObject.AddComponent<BoxCollider>();
        //    }
        //}
    }
}
