using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AI
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField]
        private Waypoint previousWaypoint;
        [SerializeField]
        private Waypoint nextWaypoint;

        [Range(0f, 5f)]
        public float width = 1f;
        public List<Waypoint> branches = new List<Waypoint>();

        [Range(-1, 1)]
        public int direction = 0;

        [Range(0f, 3f)]
        public float leftMargin = 0f;
        [Range(0f, 3f)]
        public float rightMargin = 0f;
        [Range(0f, 1f)]
        public float branchRatio = 0.5f;

        public WaypointRenderer waypointRenderer;
        public WaypointQuad waypointQuad;

        private IQuadContainer quadContainer;

        [Inject]
        public void Construct(IQuadContainer quadContainer)
        {
            this.quadContainer = quadContainer;
        }

        private void Start()
        {
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            waypointRenderer = new WaypointRenderer(this, lineRenderer);
            WaypointQuad quad = Instantiate(quadContainer.QuadTemplate);

            //waypointQuad = new WaypointQuad(this, quadContainer.QuadContainer);

            //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //sphere.transform.position = transform.position;
            //sphere.gameObject.layer = LayerMask.NameToLayer("Route");

            waypointRenderer.Render();
            quad.Setup(this, quadContainer.QuadContainer);

            //waypointQuad.Start();
        }

        public Waypoint PrevWayPoint { get => previousWaypoint; set => previousWaypoint = (Waypoint) value; }
        public Waypoint NextWayPoint { get => nextWaypoint; set => nextWaypoint = (Waypoint) value; }

        public List<Waypoint> Branches { get => branches; }

        public void AddBranch(Waypoint branch)
        {
            branches.Add(branch);
        }

        public string Id { get; set; }
        public Vector3 Position { get => transform.position; }
        public Vector3 Right { get => transform.right; }
        public Vector3 Forward { get => transform.forward; }
        public float Width { get => width; }
        public float LeftMargin { get => leftMargin; }
        public float RightMargin { get => rightMargin; }

        public Vector3 GetRandomPosition()
        {
            Vector3 minBound = transform.position + transform.right * width / 2f;
            Vector3 maxBound = transform.position - transform.right * width / 2f;

            return Vector3.Lerp(minBound, maxBound, UnityEngine.Random.Range(0f, 1f));
        }

        public static float Distance(Waypoint wp1, Waypoint wp2)
        {
            return Vector3.Distance(wp1.Position, wp2.Position);
        }
    }
}
