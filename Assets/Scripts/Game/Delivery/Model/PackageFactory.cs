using UnityEngine;
using Zenject;

namespace Delivery
{
    public class PackageFactory : MonoBehaviour, ItemFactory<PackageConfig, Package>
    {

        private Package.Factory instanceFactory;
        private PackageStore packageStore;
        private PackageSpawnPointStore packageSpawnPointStore;
        private PackageTargetPointStore packageTargetPointStore;

        private int packageCounter = 0;

        [Inject]
        public void Construct(Package.Factory instanceFactory, PackageSpawnPointStore packageSpawnPointStore, PackageTargetPointStore packageTargetPointStore)
        {
            this.instanceFactory = instanceFactory;
            this.packageSpawnPointStore = packageSpawnPointStore;
            this.packageTargetPointStore = packageTargetPointStore;
        }

        public Package Create(PackageConfig packageConfig)
        {
            GameObject targetObject = GetTargetPoint();

            Package newPackage = instanceFactory.Create(packageStore.PackageTemplate);
            newPackage.transform.position = packageConfig.spawnPoint.transform.position;
            newPackage.Name = "package-" + packageCounter++;
            newPackage.Price = packageConfig.price;
            newPackage.gameObject.SetActive(true);
            newPackage.SpawnPoint = packageConfig.spawnPoint;

            GameObject newMinimapPackage = Instantiate(packageStore.PackageMinimapTemplate, packageStore.PackageMinimapTemplate.transform.parent);
            newMinimapPackage.transform.position = packageConfig.spawnPoint.transform.position;
            newMinimapPackage.gameObject.SetActive(true);
            newMinimapPackage.transform.SetParent(gameObject.transform);
            newPackage.MinimapGameObject = newMinimapPackage;

            GameObject packageTarget = Instantiate(packageStore.PackageTargetTemplate, packageStore.PackageTargetTemplate.transform.parent);
            packageTarget.gameObject.SetActive(false);
            packageTarget.transform.position = targetObject.transform.position;
            newPackage.Target = packageTarget;

            GameObject targetMinimapGameObject = Instantiate(packageStore.PackageTargetMinimapTemplate, packageStore.PackageTargetMinimapTemplate.transform.parent);
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

