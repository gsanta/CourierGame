using UnityEngine;

public class PackageFactory : MonoBehaviour
{
    [SerializeField] private GameObject deliveryPackageTemplate;
    [SerializeField] private GameObject deliveryPackageMinimapTemplate;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] targetPoints;
    
    [HideInInspector] public DeliveryStore deliveryService;
    [HideInInspector] public PlayerFactory playerFactory;

    private int packageCounter = 0;

    public void Start()
    {
        deliveryPackageTemplate.SetActive(false);
        deliveryPackageMinimapTemplate.SetActive(false);

        foreach (GameObject spawnPoint in spawnPoints)
        {
            spawnPoint.SetActive(false);
        }
    }

    public Package CreatePackage(PackageConfig packageConfig)
    {
        GameObject targetObject = GetTargetPoint();
        Package newPackage = Instantiate(deliveryPackageTemplate.GetComponent<Package>(), deliveryPackageTemplate.transform.parent);
        newPackage.deliveryService = deliveryService;
        newPackage.transform.position = packageConfig.spawnPoint.transform.position;
        newPackage.targetObject = targetObject;
        newPackage.Name = "package-" + packageCounter++;
        newPackage.gameObject.SetActive(true);

        GameObject newMinimapPackage = Instantiate(deliveryPackageMinimapTemplate, deliveryPackageMinimapTemplate.transform.parent);
        newMinimapPackage.transform.position = packageConfig.spawnPoint.transform.position;
        newMinimapPackage.gameObject.SetActive(true);
        newMinimapPackage.transform.SetParent(gameObject.transform);

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
