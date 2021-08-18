using System;
using UnityEngine;
using Zenject;

public class Package : MonoBehaviour
{
    private PackageStore packageStore;
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
    public void Construct(PackageStore packageStore)
    {
        this.packageStore = packageStore;
    }

    public string Name { get; set; }

    public GameObject Target
    {
        set => targetObject = value;
        get => targetObject;
    }

    public void PickupBy(ICourier courier)
    {
        origParent = gameObject.transform.parent;
        gameObject.transform.SetParent(courier.GetGameObject().transform);
        Status = DeliveryStatus.ASSIGNED;
        targetObject.SetActive(true);
        HandleStatusChanged();
    }

    public void ReservePackage(ICourier courier)
    {
        Status = DeliveryStatus.RESERVED;
        courier.SetPackage(this);
        HandleStatusChanged();
    }

    public void DestroyPackage()
    {
        gameObject.SetActive(false);
        targetObject.SetActive(false);
        MinimapGameObject.SetActive(false);
        Destroy(gameObject);
        Destroy(MinimapGameObject);
    }

    public void DropPackage()
    {
        Status = DeliveryStatus.DELIVERED;

        gameObject.transform.SetParent(origParent);
        targetObject.SetActive(false);

        Vector2 packagePos = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPos = new Vector2(Target.transform.position.x, Target.transform.position.z);
            packageStore.Remove(this);
            DestroyPackage();
            Status = DeliveryStatus.DELIVERED;
        //if (Vector2.Distance(packagePos, targetPos) < 1)
        //{
        //}
        //else
        //{
        //    Status = DeliveryStatus.UNASSIGNED;
        //}
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

        OnStatusChanged?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler OnStatusChanged;


    public class Factory : PlaceholderFactory<UnityEngine.Object, Package>
    {
    }
}
