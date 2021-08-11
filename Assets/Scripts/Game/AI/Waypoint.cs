using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI
{
    public class Waypoint : MonoBehaviour
    {
        public Waypoint previousWaypoint;
        public Waypoint nextWaypoint;

        [Range(0f, 5f)]
        public float width = 1f;


        public Vector3 GetPosition()
        {
            Vector3 minBound = transform.position + transform.right * width / 2f;
            Vector3 maxBound = transform.position - transform.right * width / 2f;

            return Vector3.Lerp(minBound, maxBound, UnityEngine.Random.Range(0f, 1f));
        }
    }
}
