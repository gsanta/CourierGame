using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class TargetStoreController : MonoBehaviour
    {
        [SerializeField]
        private GameObject targetContainer;

        public void SetupStore<T>(ITargetStore<T> targetStore)
        {
            List<T> list = new List<T>();
            foreach (Transform obj in targetContainer.transform)
            {
                list.Add(obj.GetComponent<T>());
            }

            targetStore.SetTargets(list);
        }

        public void SetupStoreWithGameObjects(ITargetStore<GameObject> targetStore)
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
