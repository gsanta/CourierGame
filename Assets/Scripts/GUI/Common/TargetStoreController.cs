using Core;
using System.Collections.Generic;
using UnityEngine;

namespace GUI
{
    public class TargetStoreController : MonoBehaviour
    {
        [SerializeField]
        private GameObject targetContainer;

        public void SetupStore(ITargetStore targetStore)
        {
            List<GameObject> homes = new List<GameObject>();
            foreach (Transform obj in targetContainer.transform)
            {
                homes.Add(obj.gameObject);
            }

            targetStore.SetTargets(homes);
        }
    }
}
