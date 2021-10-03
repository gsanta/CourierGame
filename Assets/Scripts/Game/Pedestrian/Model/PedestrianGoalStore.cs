
using System.Collections.Generic;
using UnityEngine;

namespace Pedestrians
{
    public class PedestrianGoalStore : MonoBehaviour
    {
        [SerializeField]
        private GameObject goalContainer;

        private List<GameObject> goals = new List<GameObject>();

        private void Awake()
        {
            foreach (Transform obj in goalContainer.transform)
            {
                goals.Add(obj.gameObject);
            }
        }

        public List<GameObject> GetGoals()
        {
            return goals;
        }
    }
}
