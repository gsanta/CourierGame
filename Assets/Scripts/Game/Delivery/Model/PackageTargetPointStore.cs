
using Scenes;
using System.Collections.Generic;
using UnityEngine;

namespace Delivery
{
    public class PackageTargetPointStore : IResetable
    {
        private List<GameObject> targetPoints = new List<GameObject>();

        public List<GameObject> GetAll()
        {
            return targetPoints;
        }

        public void Add(GameObject targetPoint)
        {
            targetPoints.Add(targetPoint);
        }

        public void Reset()
        {
            targetPoints = new List<GameObject>();
        }

        public int Size
        {
            get => targetPoints.Count;
        }
    }
}
