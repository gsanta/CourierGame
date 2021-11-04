
using Delivery;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class PackageTargetPointStoreController : MonoBehaviour
    {
        [SerializeField]
        private GameObject container;

        private PackageTargetPointStore packageTargetPointStore;

        [Inject]
        public void Construct(PackageTargetPointStore packageTargetPointStore)
        {
            this.packageTargetPointStore = packageTargetPointStore;
        }

        private void Start()
        {
            foreach (Transform child in container.transform)
            {
                var obj = child.gameObject;
                obj.SetActive(false);
                packageTargetPointStore.Add(obj);
            }
        }
    }
}
