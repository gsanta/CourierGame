
using Delivery;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class PackageStoreController : MonoBehaviour
    {
        [SerializeField]
        private Package packageTemplate;
        [SerializeField]
        private GameObject packageTargetTemplate;
        [SerializeField]
        private GameObject packageMinimapTemplate;
        [SerializeField]
        private GameObject packageTargetMinimapTemplate;

        private PackageStore packageStore;

        [Inject]
        public void Construct(PackageStore packageStore)
        {
            this.packageStore = packageStore;
        }

        private void Start()
        {
            packageTemplate.gameObject.SetActive(false);
            packageMinimapTemplate.SetActive(false);

            packageStore.SetPackageTemplate(packageTemplate);
            packageStore.SetPackageTargetTemplate(packageTargetTemplate);
            packageStore.SetPackageMinimapTemplate(packageMinimapTemplate);
            packageStore.SetPackageTargetMinimapTemplate(packageTargetMinimapTemplate);
        }
    }
}
