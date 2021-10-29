
using Pedestrians;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class WalkTargetStoreController : MonoBehaviour
    {
        [SerializeField]
        private GameObject targetContainer;

        private WalkTargetStore walkTargetStore;

        [Inject]
        public void Construct(WalkTargetStore walkTargetStore)
        {
            this.walkTargetStore = walkTargetStore;
        }

        private void Awake()
        {
            List<walkTarget> targets = new List<walkTarget>();
            foreach (Transform obj in targetContainer.transform)
            {
                targets.Add(obj.GetComponent<walkTarget>());
            }

            walkTargetStore.SetTargets(targets);
        }
    }
}
