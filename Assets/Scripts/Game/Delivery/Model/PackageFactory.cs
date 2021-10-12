using UnityEngine;
using Zenject;

namespace Delivery
{
    public class PackageFactory : ItemFactory<PackageConfig, Package>
    {
        private PackageSpawnPointStore packageSpawnPointStore;
        private PackageTargetPointStore packageTargetPointStore;
        private IPackageInstantiator packageInstantiator;

        private int packageCounter = 0;

        [Inject]
        public void Construct(PackageSpawnPointStore packageSpawnPointStore, PackageTargetPointStore packageTargetPointStore)
        {
            this.packageSpawnPointStore = packageSpawnPointStore;
            this.packageTargetPointStore = packageTargetPointStore;
        }

        public void SetPackageInstantiator(IPackageInstantiator packageInstantiator)
        {
            this.packageInstantiator = packageInstantiator;
        }

        public Package Create(PackageConfig packageConfig)
        {
            GameObject targetObject = GetTargetPoint();

            Package newPackage = packageInstantiator.InstantitatePackage();
            newPackage.transform.position = packageConfig.spawnPoint.transform.position;
            newPackage.Name = "package-" + packageCounter++;
            newPackage.Price = packageConfig.price;
            newPackage.gameObject.SetActive(true);
            newPackage.SpawnPoint = packageConfig.spawnPoint;

            GameObject newMinimapPackage = packageInstantiator.InstantiateMinimapPackage();
            newMinimapPackage.transform.position = packageConfig.spawnPoint.transform.position;
            newMinimapPackage.gameObject.SetActive(true);
            newPackage.MinimapGameObject = newMinimapPackage;

            GameObject packageTarget = packageInstantiator.InstantiatePackageTarget();
            packageTarget.gameObject.SetActive(false);
            packageTarget.transform.position = targetObject.transform.position;
            newPackage.Target = packageTarget;

            GameObject targetMinimapGameObject = packageInstantiator.InstantiatePackageTargetOnMinimap();
            targetMinimapGameObject.transform.position = targetObject.transform.position;
            newPackage.TargetMinimapGameObject = targetMinimapGameObject;

            return newPackage;
        }

        public Transform GetSpawnPosition()
        {
            return packageSpawnPointStore.GetAll()[Random.Range(0, packageSpawnPointStore.Size)].transform;
        }

        public GameObject GetTargetPoint()
        {
            return packageTargetPointStore.GetAll()[Random.Range(0, packageTargetPointStore.Size)];
        }
    }
}

