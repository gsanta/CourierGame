using UnityEngine;
using Zenject;

public class Package : MonoBehaviour
{
    private DeliveryStore deliveryStore;
    
    private GameObject targetObject;
    private Transform origParent;

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
        Destroy(gameObject);
        Destroy(targetObject);
    }

    public class Factory : PlaceholderFactory<Object, Package>
    {
    }
}
