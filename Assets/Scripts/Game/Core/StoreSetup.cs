using Scenes;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class StoreSetup
    {
        public void SetupStore<T>(GameObject targetContainer, ITargetStore<T> targetStore)
        {
            List<T> list = new List<T>();
            foreach (Transform obj in targetContainer.transform)
            {
                list.Add(obj.GetComponent<T>());
            }

            targetStore.SetTargets(list);
        }

        public void SetupStoreWithGameObjects(GameObject targetContainer, ITargetStore<GameObject> targetStore)
        {
            List<GameObject> list = new List<GameObject>();
            foreach (Transform obj in targetContainer.transform)
            {
                list.Add(obj.gameObject);
            }

            targetStore.SetTargets(list);
        }
    }
}
