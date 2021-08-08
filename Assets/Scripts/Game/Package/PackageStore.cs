
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PackageStore
{
    private List<Package> packages = new List<Package>();

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
}
