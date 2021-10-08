
using System.Collections.Generic;
using UnityEngine;

namespace Pedestrians
{
    public class PedestrianTargetStore : MonoBehaviour
    {
        [SerializeField]
        private GameObject goalContainer;

        private List<PedestrianTarget> goals = new List<PedestrianTarget>();

        private void Awake()
        {
            foreach (Transform obj in goalContainer.transform)
            {
                goals.Add(obj.GetComponent<PedestrianTarget>());
            }
        }

        public List<PedestrianTarget> GetTargets()
        {
            return goals;
        }

        public PedestrianTarget GetByName(string name)
        {
            return goals.Find(goal => goal.name == name);
        }
    }
}
