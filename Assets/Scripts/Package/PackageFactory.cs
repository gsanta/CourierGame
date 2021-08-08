using UnityEngine;
using Zenject;

public class PackageFactory : MonoBehaviour, ItemFactory<PackageConfig, Package>
{
    [SerializeField] private Package packageTemplate;
    [SerializeField] private GameObject deliveryPackageMinimapTemplate;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] targetPoints;
    
    private Package.Factory instanceFactory;

    private int packageCounter = 0;

    [Inject]
    public void Construct(Package.Factory instanceFactory)
    {
        this.instanceFactory = instanceFactory;
    }

    public void Start()
    {
        packageTemplate.gameObject.SetActive(false);
        deliveryPackageMinimapTemplate.SetActive(false);

        foreach (GameObject spawnPoint in spawnPoints)
        {
            spawnPoint.SetActive(false);
        }
    }

    public Package Create(PackageConfig packageConfig)
    {
        GameObject targetObject = GetTargetPoint();
        Package newPackage = instanceFactory.Create(packageTemplate);
        newPackage.transform.position = packageConfig.spawnPoint.transform.position;
        newPackage.Target = targetObject;
        newPackage.Name = "package-" + packageCounter++;
        newPackage.gameObject.SetActive(true);

        GameObject newMinimapPackage = Instantiate(deliveryPackageMinimapTemplate, deliveryPackageMinimapTemplate.transform.parent);
        newMinimapPackage.transform.position = packageConfig.spawnPoint.transform.position;
        newMinimapPackage.gameObject.SetActive(true);
        newMinimapPackage.transform.SetParent(gameObject.transform);

        newPackage.MinimapGameObject = newMinimapPackage;

        return newPackage;
    }

    public Transform GetSpawnPosition()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
    }

    public GameObject GetTargetPoint()
    {
        return targetPoints[Random.Range(0, targetPoints.Length)];
    }
}
