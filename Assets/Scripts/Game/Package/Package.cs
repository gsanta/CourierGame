using System;
using UnityEngine;
using Zenject;

public class Package : MonoBehaviour
{
    private PackageStore packageStore;
    private PackageTarget targetObject;
    private ICourier courier;

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

    public PackageTarget Target
    {
        set => targetObject = value;
        get => targetObject;
    }

    public void PickupBy(ICourier courier)
    {
        if (this.courier == courier)
        {
            gameObject.transform.SetParent(courier.GetGameObject().transform);
            Status = DeliveryStatus.ASSIGNED;
            targetObject.SetMeshVisibility(true);
            HandleStatusChanged();
        }
    }

    public void ReservePackage(ICourier courier)
    {
        if (this.courier == null)
        {
            this.courier = courier;
            Status = DeliveryStatus.RESERVED;
            courier.SetPackage(this);
            HandleStatusChanged();
        }
    }

    public void DestroyPackage()
    {
        gameObject.SetActive(false);
        targetObject.SetMeshVisibility(false);
        MinimapGameObject.SetActive(false);
        Destroy(gameObject);
        Destroy(MinimapGameObject);
    }

    public void DeliverPackage()
    {
        Vector2 packagePos = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPos = new Vector2(Target.transform.position.x, Target.transform.position.z);

        if (Vector2.Distance(packagePos, targetPos) < 1)
        {
            targetObject.SetMeshVisibility(false);
            packageStore.Remove(this);
            courier.SetPackage(null);
            courier = null;

            Status = DeliveryStatus.DELIVERED;

            DestroyPackage();
        }
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

        StatusChanged?.Invoke(this, new PackageStatusChangedEventArgs(this));
    }

    public event EventHandler<PackageStatusChangedEventArgs> StatusChanged;


    public class Factory : PlaceholderFactory<UnityEngine.Object, Package>
    {
    }
}

public class PackageStatusChangedEventArgs : EventArgs
{
    private readonly Package package;

    public PackageStatusChangedEventArgs(Package package)
    {
        this.package = package;
    }
    
    public Package Package { get => package; }
}
