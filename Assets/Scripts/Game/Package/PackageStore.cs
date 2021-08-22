
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PackageStore : MonoBehaviour
{
    private List<Package> packages = new List<Package>();

    [SerializeField] private GameObject[] packageSpawnPoints;
    [SerializeField] private PackageTarget[] packageTargetPoints;
    [SerializeField] private Package packageTemplate;
    [SerializeField] private GameObject deliveryPackageMinimapTemplate;

    public GameObject[] PackageSpawnPoints
    {
        get => packageSpawnPoints;
    }

    public PackageTarget[] PackageTargetPoints
    {
        get => packageTargetPoints;
    }

    public Package PackageTemplate
    {
        get => packageTemplate;
    }

    public GameObject DeliveryPackageMinimapTemplate
    {
        get => deliveryPackageMinimapTemplate;
    }

    public void Add(Package package)
    {
        packages.Add(package);
        TriggerPackageAdded(new PackageAddedEventArgs(package));
    }

    public void Remove(Package package)
    {
        packages.Remove(package);
    }

    public List<Package> GetAll()
    {
        return packages;
    }

    public List<Package> GetAllPickable()
    {
        return GetPackagesOfStatus(DeliveryStatus.UNASSIGNED);
    }

    public List<Package> GetPackagesOfStatus(DeliveryStatus status, params DeliveryStatus[] rest)
    {
        DeliveryStatus[] statuses = new DeliveryStatus[rest.Length + 1];
        statuses[0] = status;
        rest.CopyTo(statuses, 1);

        return packages.FindAll(package => statuses.Contains(package.Status));
    }

    public bool GetPackageWithinPickupRange(ICourier courier, out Package deliveryPackage)
    {
        foreach (var package in packages)
        {
            if (Vector3.Distance(courier.GetTransform().position, package.transform.position) < 2)
            {
                deliveryPackage = package;
                return true;
            }
        }

        deliveryPackage = null;
        return false;
    }

    public event EventHandler<PackageAddedEventArgs> OnPackageAdded;

    private void TriggerPackageAdded(PackageAddedEventArgs e)
    {
        EventHandler<PackageAddedEventArgs> handler = OnPackageAdded;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    public void Start()
    {
        PackageTemplate.gameObject.SetActive(false);
        DeliveryPackageMinimapTemplate.SetActive(false);

        foreach (GameObject spawnPoint in PackageSpawnPoints)
        {
            spawnPoint.SetActive(false);
        }

        foreach (PackageTarget target in packageTargetPoints)
        {
            target.SetMeshVisibility(false);
        }
    }
}

public class PackageAddedEventArgs : EventArgs
{
    private Package package;

    internal PackageAddedEventArgs(Package package)
    {
        this.package = package;
    }
    public Package Package { get => package; }
}

