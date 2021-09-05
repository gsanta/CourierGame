using System;
using UnityEngine;
using Zenject;

public class Package : MonoBehaviour
{
    private PackageStore packageStore;
    private PackageTarget targetObject;

    public GameObject SpawnPoint { get; set; }

    public GameObject MinimapGameObject { get; set; }
    public GameObject TargetMinimapGameObject { get; set; }

    public int Price { set; get; }

    private DeliveryStatus status = DeliveryStatus.UNASSIGNED;

    public Biker Biker { set; get; }

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

    public void DestroyPackage()
    {
        gameObject.SetActive(false);
        targetObject.gameObject.SetActive(false);
        MinimapGameObject.SetActive(false);
        Destroy(gameObject);
        Destroy(MinimapGameObject);
        Destroy(TargetMinimapGameObject);
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

    public class Factory : PlaceholderFactory<UnityEngine.Object, Package>
    {
    }
}