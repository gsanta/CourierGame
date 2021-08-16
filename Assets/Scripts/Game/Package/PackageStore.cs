
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PackageStore : MonoBehaviour
{
    private List<Package> packages = new List<Package>();

    [SerializeField] private GameObject[] packageSpawnPoints;
    [SerializeField] private GameObject[] packageTargetPoints;
    [SerializeField] private Package packageTemplate;
    [SerializeField] private GameObject deliveryPackageMinimapTemplate;

    public GameObject[] PackageSpawnPoints
    {
        get => packageSpawnPoints;
    }

    public GameObject[] PackageTargetPoints
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
        OnPackageAdded?.Invoke(this, EventArgs.Empty);
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
        return GetPackagesOfStatus(Package.DeliveryStatus.UNASSIGNED);
    }

    public List<Package> GetPackagesOfStatus(Package.DeliveryStatus status, params Package.DeliveryStatus[] rest)
    {
        Package.DeliveryStatus[] statuses = new Package.DeliveryStatus[rest.Length + 1];
        statuses[0] = status;
        rest.CopyTo(statuses, 1);

        return packages.FindAll(package => statuses.Contains(package.Status));
    }

    public bool GetPackageWithinPickupRange(Player playerController, out Package deliveryPackage)
    {
        foreach (var package in packages)
        {
            if (Vector3.Distance(playerController.transform.position, package.transform.position) < 2)
            {
                deliveryPackage = package;
                return true;
            }
        }

        deliveryPackage = null;
        return false;
    }

    public event EventHandler OnPackageAdded;

    public void Start()
    {
        PackageTemplate.gameObject.SetActive(false);
        DeliveryPackageMinimapTemplate.SetActive(false);

        foreach (GameObject spawnPoint in PackageSpawnPoints)
        {
            spawnPoint.SetActive(false);
        }
    }
}
