using Controls;
using UnityEngine;
using Zenject;

namespace Bikers
{
    public class BikersConfigHandler : MonoBehaviour
    {
        [SerializeField]
        private int bikerCount;
        [SerializeField]
        private MinimapBiker minimapBiker;
        [SerializeField]
        private GameObject spawnPointContainer;
        [SerializeField]
        private Player bikerTemplate;
        [SerializeField]
        private GameObject bikerContainer;

        private BikersConfig bikersConfig;
        private BikerStore bikerStore;
        private StoreSetup storeSetup;
        private BikerHomeStore bikerHomeStore;

        [Inject]
        public void Construct(BikersConfig bikersConfig, BikerStore bikerStore, BikerHomeStore bikerHomeStore, StoreSetup storeSetup)
        {
            this.bikersConfig = bikersConfig;
            this.bikerStore = bikerStore;
            this.bikerHomeStore = bikerHomeStore;
            this.storeSetup = storeSetup;
        }

        private void Awake()
        {
            bikersConfig.BikerCount = bikerCount;
            bikerStore.SetBikerTemplate(bikerTemplate);
            bikerStore.SetMinimapBiker(minimapBiker);
            bikerStore.SetBikerContainer(bikerContainer);
            storeSetup.SetupStoreWithGameObjects(spawnPointContainer, bikerHomeStore);
        }
    }
}
