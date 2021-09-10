
using System.Collections.Generic;
using UnityEngine;

namespace Domain
{
    public class Steering
    {
        public Dictionary<GameObject, int> avoidedGameObjects = new Dictionary<GameObject, int>();

        private readonly GameObject gameObject;
        public float seeAhead = 5f;
        public float radius = 1f;
        public float maxAvoidanceForce = 5f;

        public Steering(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public Vector3 Center { get => gameObject.transform.position; }

        public Vector3 GetCenterTo(Steering other) {
            return gameObject.transform.position - other.Center;
        }

        private Vector3 Ahead()
        {
            return gameObject.transform.forward * seeAhead;
        }

        private Vector3 Ahead2()
        {
            return gameObject.transform.forward * seeAhead / 2;
        }

        private float DistToAhead(Steering other)
        {
            return Vector3.Distance(Ahead(), other.GetCenterTo(this));
        }

        private float DistToAhead2(Steering other)
        {
            return Vector3.Distance(Ahead2(), other.GetCenterTo(this));
        }

        public float Distance(Steering other)
        {
            return Vector3.Distance(Center, other.Center);
        }

        public bool Intersects(Steering other)
        {
            return DistToAhead(other) < other.radius || DistToAhead2(other) < other.radius;
        }

        public Vector3 GetAvoidance(Steering other)
        {
            return (Ahead() - other.GetCenterTo(this)).normalized * maxAvoidanceForce;
        }
    }
}
