
using Delivery;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class PackageInstantiator : MonoBehaviour, IPackageInstantiator
    {
        private PackageStore packageStore;
        private Package.Factory instanceFactory;

        [Inject]
        public void Construct(PackageFactory packageFactory, Package.Factory instanceFactory, PackageStore packageStore)
        {
            this.instanceFactory = instanceFactory;
            this.packageStore = packageStore;

            packageFactory.SetPackageInstantiator(this);
        }

        public Package InstantitatePackage()
        {
            return instanceFactory.Create(packageStore.GetPackageTemplate());
        }

        public GameObject InstantiateMinimapPackage()
        {
            var gameObject = Instantiate(packageStore.GetPackageMinimapTemplate(), packageStore.GetPackageMinimapTemplate().transform.parent);
            gameObject.transform.SetParent(gameObject.transform);
            return gameObject;
        }

        public GameObject InstantiatePackageTarget()
        {
            return Instantiate(packageStore.GetPackageTargetTemplate(), packageStore.GetPackageTargetTemplate().transform.parent);
        }

        public GameObject InstantiatePackageTargetOnMinimap()
        {
            return Instantiate(packageStore.GetPackageTargetMinimapTemplate(), packageStore.GetPackageTargetMinimapTemplate().transform.parent);
        }
    }
}
