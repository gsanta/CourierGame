using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{

    public interface ItemToPositionConverter<T> {
        Vector3 GetPosition(T item);
    }

    public class NearestItemCalc<T>
    {
        private ItemToPositionConverter<T> converter;

        public NearestItemCalc(ItemToPositionConverter<T> converter)
        {
            this.converter = converter;
        }

        public T GetNearest(T target, List<T> sources)
        {
            double minDistance = Double.MaxValue;
            T minDistItem = default(T);

            var targetPos = converter.GetPosition(target);

            foreach (var source in sources)
            {
                var sourcePos = converter.GetPosition(source);
                var dist = Vector3.Distance(targetPos, sourcePos);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    minDistItem = source;
                }
            }

            return minDistItem;
        }
    }
}
