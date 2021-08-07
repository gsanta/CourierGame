

using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryStore
{
    private Dictionary<Player, Package> packageMap = new Dictionary<Player, Package>();
    private Dictionary<Package, Player> reversePackageMap = new Dictionary<Package, Player>();
    private PackageStore packageStore;

    public DeliveryStore(PackageStore packageStore)
    {
        this.packageStore = packageStore;
    }

    public void AssignPackageToPlayer(Player player, Package package)
    {
        packageMap.Add(player, package);
        reversePackageMap.Add(package, player);

        OnDeliveryStatusChanged?.Invoke(this, EventArgs.Empty);
    }

    public void DropPackage(Package package)
    {
        Player player;
        if (reversePackageMap.TryGetValue(package, out player))
        {
            packageMap.Remove(player);
        }
        reversePackageMap.Remove(package);

        if (Vector3.Distance(package.transform.position, package.targetObject.transform.position) < 1)
        {
            packageStore.Remove(package);
            package.DestroyPackage();
        } 
        
        OnDeliveryStatusChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool GetPackage(Player player, out Package package)
    {
        return packageMap.TryGetValue(player, out package);
    }

    public List<Package> AssignedPackages
    {
        get => packageStore.GetAll().FindAll(package => GetPlayerForPackage(package) != null);
    }

    public List<Package> UnAssignedPackages
    {
        get => packageStore.GetAll().FindAll(package => GetPlayerForPackage(package) == null);
    }

    public Player GetPlayerForPackage(Package package)
    {
        Player player;

        reversePackageMap.TryGetValue(package, out player);

        return player;
    }

    public event EventHandler OnDeliveryStatusChanged;
}
