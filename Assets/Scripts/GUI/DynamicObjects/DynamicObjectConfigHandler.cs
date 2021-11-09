using Delivery;
using UnityEngine;
using Zenject;

namespace GUI
{

    public class DynamicObjectConfigHandler : MonoBehaviour
    {
        [SerializeField]
        private Package packageTemplate;
        [SerializeField]
        private GameObject packageTargetTemplate;
        [SerializeField]
        private GameObject packageMinimapTemplate;
        [SerializeField]
        private GameObject packageTargetMinimapTemplate;
        [SerializeField]
        private GameObject packageTargetContainer;
        [SerializeField]
        private GameObject packageSpawnPointContainer;

        private PackageStore packageStore;
        private PackageTargetPointStore packageTargetPointStore;
        private PackageSpawnPointStore packageSpawnPointStore;

        [Inject]
        public void Construct(PackageStore packageStore, PackageTargetPointStore packageTargetPointStore, PackageSpawnPointStore packageSpawnPointStore)
        {
            this.packageStore = packageStore;
            this.packageTargetPointStore = packageTargetPointStore;
            this.packageSpawnPointStore = packageSpawnPointStore;
        }

        private void Start()
        {
            packageTemplate.gameObject.SetActive(false);
            packageMinimapTemplate.SetActive(false);

            packageStore.SetPackageTemplate(packageTemplate);
            packageStore.SetPackageTargetTemplate(packageTargetTemplate);
            packageStore.SetPackageMinimapTemplate(packageMinimapTemplate);
            packageStore.SetPackageTargetMinimapTemplate(packageTargetMinimapTemplate);

            foreach (Transform child in packageTargetContainer.transform)
            {
                var obj = child.gameObject;
                obj.SetActive(false);
                packageTargetPointStore.Add(obj);
            }

            foreach (Transform child in packageSpawnPointContainer.transform)
            {
                var obj = child.gameObject;
                obj.SetActive(false);
                packageSpawnPointStore.Add(obj);
            }
        }
    }
}
