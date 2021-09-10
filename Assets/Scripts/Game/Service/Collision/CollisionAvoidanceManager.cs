using System.Collections.Generic;
using UnityEngine;

namespace Domain
{
    struct ActiveAvoidance
    {
        public GameObject obj1;
        public GameObject obj2;
        public float countDown;

        public ActiveAvoidance(GameObject obj1, GameObject obj2, float countDown)
        {
            this.obj1 = obj1;
            this.obj2 = obj2;
            this.countDown = countDown;
        }
    }

    public class CollisionAvoidanceManager : MonoBehaviour
    {
        private Dictionary<GameObject, HashSet<GameObject>> avoidanceMap = new Dictionary<GameObject, HashSet<GameObject>>();

        private List<ActiveAvoidance> avoidanceList = new List<ActiveAvoidance>();

        public void AddAvoidance(GameObject obj1, GameObject obj2)
        {
            AddToMap(obj1, obj2);
            AddToMap(obj2, obj1);
            avoidanceList.Add(new ActiveAvoidance(obj1, obj2, 3));
        }

        private void Update()
        {
            var toRemove = new List<ActiveAvoidance>();

            avoidanceList.ForEach(item =>
            {
                item.countDown -= Time.deltaTime;

                if (item.countDown < 0)
                {
                    toRemove.Add(item);
                }
            });

            RemoveFromAvoidanceMap(toRemove);
        }

        private void RemoveFromAvoidanceMap(List<ActiveAvoidance> toRemove)
        {
            toRemove.ForEach(item =>
            {
                avoidanceList.Remove(item);

                RemoveFromMap(item.obj1, item.obj2);
                RemoveFromMap(item.obj2, item.obj1);
            });
        }

        private void RemoveFromMap(GameObject source, GameObject target)
        {
            if (avoidanceMap[source].Contains(target))
            {
                avoidanceMap[source].Remove(target);
            }

            if (avoidanceMap[source].Count == 0)
            {
                avoidanceMap.Remove(source);
            }
        }

        private void AddToMap(GameObject source, GameObject target)
        {
            if (!avoidanceMap.ContainsKey(source))
            {
                avoidanceMap.Add(source, new HashSet<GameObject>());
            }

            avoidanceMap[source].Add(target);
        }
    }
}
