using UnityEngine;
using Zenject;

public class Package : MonoBehaviour
{
    public enum DeliveryStatus
    {
        UNASSIGNED,
        ASSIGNED,
        DELIVERED
    }

    private DeliveryStore deliveryStore;
    private GameObject targetObject;
    private Transform origParent;
    public GameObject SpawnPoint { get; set; }

    public GameObject MinimapGameObject { get; set; }

    private DeliveryStatus status = DeliveryStatus.UNASSIGNED;

    public DeliveryStatus Status
    {
        set
        {
            status = value;
            HandleStatusChanged();
        }

        get => status;
    }

    [Inject]
    public void Construct(DeliveryStore deliveryStore)
    {
        this.deliveryStore = deliveryStore;
    }

    public string Name { get; set; }

    public GameObject Target
    {
        set => targetObject = value;
        get => targetObject;
    }

    public void PickupBy(Player player)
    {
        origParent = gameObject.transform.parent;
        gameObject.transform.SetParent(player.transform);
        deliveryStore.AssignPackageToPlayer(player, this);
        targetObject.SetActive(true);
    }

    public void ReleasePackage()
    {
        deliveryStore.DropPackage(this);
        gameObject.transform.SetParent(origParent);
        targetObject.SetActive(false);
    }

    public void DestroyPackage()
    {
        gameObject.SetActive(false);
        targetObject.SetActive(false);
        MinimapGameObject.SetActive(false);
        Destroy(gameObject);
        Destroy(MinimapGameObject);
    }

    private void HandleStatusChanged()
    {
        switch (status)
        {
            case DeliveryStatus.ASSIGNED:
                MinimapGameObject.SetActive(false);
                break;
            case DeliveryStatus.UNASSIGNED:
                MinimapGameObject.SetActive(true);
                break;
        }
    }

    public class Factory : PlaceholderFactory<Object, Package>
    {


    }
}
