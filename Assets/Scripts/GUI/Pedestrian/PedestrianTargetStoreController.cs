
using Pedestrians;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class PedestrianTargetStoreController : MonoBehaviour
    {
        [SerializeField]
        private GameObject goalContainer;

        private PedestrianTargetStore pedestrianTargetStore;

        [Inject]
        public void Construct(PedestrianTargetStore pedestrianTargetStore)
        {
            this.pedestrianTargetStore = pedestrianTargetStore;
        }

        private void Awake()
        {
            List<PedestrianTarget> targets = new List<PedestrianTarget>();
            foreach (Transform obj in goalContainer.transform)
            {
                targets.Add(obj.GetComponent<PedestrianTarget>());
            }

            pedestrianTargetStore.SetTargets(targets);
        }
    }
}
