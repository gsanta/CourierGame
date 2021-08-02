using UnityEngine;

public class DeliveryPackageController : MonoBehaviour
{

    [SerializeField] private GameObject deliveryPackageTemplate;
    [SerializeField] private GameObject deliveryPackageMinimapTemplate;
    [SerializeField] private GameObject[] spawnPoints;
    
    [HideInInspector] public DeliveryService deliveryService;

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

    public void SpawnPackage()
    {
        Transform transform = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
        DeliveryPackage newPackage = Instantiate(deliveryPackageTemplate.GetComponent<DeliveryPackage>(), deliveryPackageTemplate.transform.parent);
        newPackage.deliveryService = deliveryService;
        newPackage.transform.position = transform.position;
        newPackage.gameObject.SetActive(true);
        newPackage.transform.SetParent(gameObject.transform);

        MinimapDeliveryPackage newMinimapPackage = Instantiate(deliveryPackageMinimapTemplate.GetComponent<MinimapDeliveryPackage>(), deliveryPackageMinimapTemplate.transform.parent);
        newMinimapPackage.transform.position = transform.position;
        newMinimapPackage.gameObject.SetActive(true);
        newMinimapPackage.transform.SetParent(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
