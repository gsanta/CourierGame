using UnityEngine;

public class Package : MonoBehaviour
{
    [HideInInspector] public PlayerFactory playerFactory;
    [HideInInspector] public DeliveryStore deliveryService;
    [HideInInspector] public GameObject targetObject;

    private Transform origParent;

    public string Name { get; set; }

    public void PickupBy(Player player)
    {
        origParent = gameObject.transform.parent;
        gameObject.transform.SetParent(player.transform);
        deliveryService.AssignPackageToPlayer(player, this);
        targetObject.SetActive(true);
    }

    public void ReleasePackage()
    {
        deliveryService.DropPackage(this);
        gameObject.transform.SetParent(origParent);
        targetObject.SetActive(false);
    }

    public void DestroyPackage()
    {
        Destroy(gameObject);
        Destroy(targetObject);
    }
}
