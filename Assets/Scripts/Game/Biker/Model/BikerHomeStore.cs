
using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Bikers
{
    public class BikerHomeStore : IResetable, ITargetStore<GameObject>
    {
        private List<GameObject> homes;
        private Dictionary<GameObject, int> homeUsers = new Dictionary<GameObject, int>();

        public void SetTargets(List<GameObject> targets)
        {
            homes = targets;
            homes.ForEach(home => homeUsers.Add(home, 0));
        }

        public GameObject ChooseHome()
        {
            var homeIndex = Random.Range(0, homes.Count - 1);
            homeUsers[homes[homeIndex]]++;
            return homes[homeIndex];
        }

        public void Reset()
        {
            homes = new List<GameObject>();
            homeUsers = new Dictionary<GameObject, int>();
        }
    }
}
