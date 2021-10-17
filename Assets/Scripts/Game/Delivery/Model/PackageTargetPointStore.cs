
using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Delivery
{
    public class PackageTargetPointStore : IClearableStore
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

        public void Clear()
        {
            targetPoints = new List<GameObject>();
        }

        public int Size
        {
            get => targetPoints.Count;
        }
    }
}
