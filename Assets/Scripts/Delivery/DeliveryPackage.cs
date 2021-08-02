using UnityEngine;

public class DeliveryPackage : MonoBehaviour
{
    [HideInInspector] public PlayerService playerService;
    [HideInInspector] public DeliveryService deliveryService;

    private Transform origParent;

    public void PickupBy(PlayerController player)
    {
        gameObject.transform.SetParent(player.transform);
        deliveryService.AssignPackageToPlayer(player, this);
    }

    public void ReleasePackage()
    {
        transform.SetParent(origParent);
        deliveryService.RemovePackageFromPlayer(this);
    }
}
