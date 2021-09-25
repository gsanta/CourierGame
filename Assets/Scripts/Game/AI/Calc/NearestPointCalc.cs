using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{

    public interface ItemToPositionConverter<T> {
        Vector3 GetPosition(T item);
    }

    public class NearestItemCalc<T, U>
    {
        private Func<T, Vector3> targetConverter;
        private Func<U, Vector3> sourceConverter;

        public NearestItemCalc(Func<T, Vector3> targetConverter, Func<U, Vector3> sourceConverter)
        {
            this.targetConverter = targetConverter;
            this.sourceConverter = sourceConverter;
        }

        public U GetNearest(T target, List<U> sources)
        {
            double minDistance = Double.MaxValue;
            U minDistItem = default(U);

            var targetPos = targetConverter(target);

            foreach (var source in sources)
            {
                var sourcePos = sourceConverter(source);
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
