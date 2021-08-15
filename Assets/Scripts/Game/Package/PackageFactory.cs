using UnityEngine;
using Zenject;

public class PackageFactory : MonoBehaviour, ItemFactory<PackageConfig, Package>
{
    
    private Package.Factory instanceFactory;
    private PackageStore packageStore;

    private int packageCounter = 0;

    [Inject]
    public void Construct(Package.Factory instanceFactory, PackageStore packageStore)
    {
        this.instanceFactory = instanceFactory;
        this.packageStore = packageStore;
    }

    public Package Create(PackageConfig packageConfig)
    {
        GameObject targetObject = GetTargetPoint();
        Package newPackage = instanceFactory.Create(packageStore.PackageTemplate);
        newPackage.transform.position = packageConfig.spawnPoint.transform.position;
        newPackage.Target = targetObject;
        newPackage.Name = "package-" + packageCounter++;
        newPackage.gameObject.SetActive(true);
        newPackage.SpawnPoint = packageConfig.spawnPoint;

        GameObject newMinimapPackage = Instantiate(packageStore.DeliveryPackageMinimapTemplate, packageStore.DeliveryPackageMinimapTemplate.transform.parent);
        newMinimapPackage.transform.position = packageConfig.spawnPoint.transform.position;
        newMinimapPackage.gameObject.SetActive(true);
        newMinimapPackage.transform.SetParent(gameObject.transform);

        newPackage.MinimapGameObject = newMinimapPackage;

        return newPackage;
    }

    public Transform GetSpawnPosition()
    {
        return packageStore.PackageSpawnPoints[Random.Range(0, packageStore.PackageSpawnPoints.Length)].transform;
    }

    public GameObject GetTargetPoint()
    {
        return packageStore.PackageTargetPoints[Random.Range(0, packageStore.PackageTargetPoints.Length)];
    }
}
