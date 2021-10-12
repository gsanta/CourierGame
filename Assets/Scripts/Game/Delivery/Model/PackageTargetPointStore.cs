
using System.Collections.Generic;
using UnityEngine;

namespace Delivery
{
    public class PackageTargetPointStore
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

        public int Size
        {
            get => targetPoints.Count;
        }
    }
}
