using UnityEngine;

public class DeliveryPackage : MonoBehaviour
{
    [HideInInspector] public PlayerService playerService;
    [HideInInspector] public DeliveryService deliveryService;
    [HideInInspector] public GameObject targetObject;

    private Transform origParent;

    public string Name { get; set; }

    public void PickupBy(PlayerController player)
    {
        gameObject.transform.SetParent(player.transform);
        deliveryService.AssignPackageToPlayer(player, this);
        targetObject.SetActive(true);
    }

    public void ReleasePackage()
    {
        transform.SetParent(origParent);
        deliveryService.RemovePackageFromPlayer(this);
        targetObject.SetActive(false);
    }
}
