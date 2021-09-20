using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Waypoint : MonoBehaviour, IWaypoint
    {
        private IWaypoint previousWaypoint;
        private IWaypoint nextWaypoint;

        [Range(0f, 5f)]
        public float width = 1f;
        public List<IWaypoint> branches = new List<IWaypoint>();

        [Range(0f, 3f)]
        public float leftMargin = 0f;
        [Range(0f, 3f)]
        public float rightMargin = 0f;
        [Range(0f, 1f)]
        public float branchRatio = 0.5f;

        public IWaypoint PrevWayPoint { get => previousWaypoint; set => previousWaypoint = value; }
        public IWaypoint NextWayPoint { get => nextWaypoint; set => nextWaypoint = value; }

        public List<IWaypoint> Branches { get => branches; }

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

        public static float Distance(IWaypoint wp1, IWaypoint wp2)
        {
            return Vector3.Distance(wp1.Position, wp2.Position);
        }
    }
}
