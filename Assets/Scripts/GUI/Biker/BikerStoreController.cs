using Bikers;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class BikerStoreController : MonoBehaviour
    {
        private BikerStore bikerStore;

        [SerializeField]
        private GameObject spawnPointContainer;
        [SerializeField]
        private Biker bikerTemplate;

        [Inject]
        public void Construct(BikerStore bikerStore)
        {
            this.bikerStore = bikerStore;
        }

        private void Start()
        {
            List<GameObject> spawnPoints = new List<GameObject>();
            
            foreach (Transform child in spawnPointContainer.transform)
            {
                child.gameObject.SetActive(false);
                spawnPoints.Add(child.gameObject);
            }

            bikerStore.SetSpawnPoints(spawnPoints.ToArray());
            bikerStore.SetBikerTemplate(bikerTemplate);
        }
    }
}
