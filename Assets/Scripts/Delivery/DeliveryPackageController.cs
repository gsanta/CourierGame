using System.Collections.Generic;
using UnityEngine;

public class DeliveryPackageController : MonoBehaviour
{

    [SerializeField] private GameObject deliveryPackageTemplate;
    [SerializeField] private GameObject deliveryPackageMinimapTemplate;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] targetPoints;
    
    [HideInInspector] public DeliveryService deliveryService;
    [HideInInspector] public PlayerService playerService;

    private List<DeliveryPackage> packages = new List<DeliveryPackage>();
    private int packageCounter = 0;

    public void Start()
    {
        deliveryPackageTemplate.SetActive(false);
        deliveryPackageMinimapTemplate.SetActive(false);

        foreach (GameObject spawnPoint in spawnPoints)
        {
            spawnPoint.SetActive(false);
        }

        SpawnPackage();
    }

    public bool GetPackageWithinPickupRange(PlayerController playerController, out DeliveryPackage deliveryPackage)
    {
        foreach (var package in packages)
        {
            if (Vector3.Distance(playerController.transform.position, package.transform.position) < 2)
            {
                deliveryPackage = package;
                return true;
            }
        }

        deliveryPackage = null;
        return false;
    }

    public void SpawnPackage()
    {
        Transform spawnPointPosition = GetSpawnPosition();
        GameObject targetObject = GetTargetPoint();
        DeliveryPackage newPackage = Instantiate(deliveryPackageTemplate.GetComponent<DeliveryPackage>(), deliveryPackageTemplate.transform.parent);
        newPackage.deliveryService = deliveryService;
        newPackage.transform.position = spawnPointPosition.position;
        newPackage.targetObject = targetObject;
        newPackage.Name = "package-" + packageCounter++;
        newPackage.gameObject.SetActive(true);
        packages.Add(newPackage);
        deliveryService.AddPackage(newPackage);

        MinimapDeliveryPackage newMinimapPackage = Instantiate(deliveryPackageMinimapTemplate.GetComponent<MinimapDeliveryPackage>(), deliveryPackageMinimapTemplate.transform.parent);
        newMinimapPackage.transform.position = spawnPointPosition.position;
        newMinimapPackage.gameObject.SetActive(true);
        newMinimapPackage.transform.SetParent(gameObject.transform);
    }

    public Transform GetSpawnPosition()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
    }

    public GameObject GetTargetPoint()
    {
        return targetPoints[Random.Range(0, targetPoints.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
