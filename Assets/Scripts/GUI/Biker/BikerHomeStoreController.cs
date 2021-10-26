using Bikers;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class BikerHomeStoreController : MonoBehaviour
    {
        [SerializeField]
        private GameObject homeContainer;

        private BikerHomeStore bikerHomeStore;

        [Inject]
        public void Construct(BikerHomeStore bikerHomeStore)
        {
            this.bikerHomeStore = bikerHomeStore;
        }

        private void Awake()
        {
            List<GameObject> homes = new List<GameObject>();
            foreach (Transform obj in homeContainer.transform)
            {
                homes.Add(obj.gameObject);
            }

            bikerHomeStore.SetHomes(homes);
        }
    }
}
