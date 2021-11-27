using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{

    public interface ItemToPositionConverter<T> {
        Vector3 GetPosition(T item);
    }

    public class NearestItemCalc<T> where T : class, IMonoBehaviour
    {
        public T GetNearest(Vector3 target, List<T> nodes)
        {
            double minDistance = Double.MaxValue;
            T minDistItem = default(T);

            foreach (var node in nodes)
            {
                var nodePos = node.GetMonoBehaviour().transform.position;
                var dist = Vector3.Distance(target, nodePos);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    minDistItem = node;
                }
            }

            return minDistItem;
        }
    }
}
