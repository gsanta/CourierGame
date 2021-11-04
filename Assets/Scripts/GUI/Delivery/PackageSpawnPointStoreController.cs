
using Delivery;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class PackageSpawnPointStoreController : MonoBehaviour
    {
        [SerializeField]
        private GameObject container;

        private PackageSpawnPointStore packageSpawnPointStore;

        [Inject]
        public void Construct(PackageSpawnPointStore packageSpawnPointStore)
        {
            this.packageSpawnPointStore = packageSpawnPointStore;
        }

        private void Start()
        {
            foreach (Transform child in container.transform)
            {
                var obj = child.gameObject;
                obj.SetActive(false);
                packageSpawnPointStore.Add(obj);
            }
        }
    }
}
