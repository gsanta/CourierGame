﻿
using UnityEngine;

namespace Domain
{
    public class Steering
    {
        private readonly GameObject gameObject;
        public float seeAhead = 5f;
        public float radius = 2f;

        public Steering(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public Vector3 Center { get => gameObject.transform.position; }

        public Vector3 GetCenterTo(Steering other) {
            return gameObject.transform.position - other.Center;
        }

        public Vector3 Ahead()
        {
            return gameObject.transform.forward * seeAhead;
        }

        public Vector3 Ahead2()
        {
            return gameObject.transform.forward * seeAhead / 2;
        }

        public float DistToAhead(Steering other)
        {
            return Vector3.Distance(Ahead(), other.GetCenterTo(this));
        }

        public float DistToAhead2(Steering other)
        {
            return Vector3.Distance(Ahead2(), other.GetCenterTo(this));
        }

        public bool Intersects(Steering other)
        {
            return DistToAhead(other) < other.radius || DistToAhead2(other) < other.radius;
        }

        public Vector3 GetAvoidance(Steering other)
        {
            return Ahead() - other.GetCenterTo(this);
        }
    }
}