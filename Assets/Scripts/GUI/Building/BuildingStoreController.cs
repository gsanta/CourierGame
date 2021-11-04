
using Buildings;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class BuildingStoreController : MonoBehaviour
    {
        [SerializeField]
        private GameObject buildingContainer;
        private BuildingStore buildingStore;

        [Inject]
        public void Construct(BuildingStore buildingStore)
        {
            this.buildingStore = buildingStore;
        }

        private void Awake()
        {
            buildingStore.SetBuildingContainer(buildingContainer);
        }
    }
}
