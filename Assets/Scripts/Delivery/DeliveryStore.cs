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
        package.Status = Package.DeliveryStatus.ASSIGNED;
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

        Vector2 packagePos = new Vector2(package.transform.position.x, package.transform.position.z);
        Vector2 targetPos = new Vector2(package.Target.transform.position.x, package.Target.transform.position.z);
        if (Vector2.Distance(packagePos, targetPos) < 1)
        {
            packageStore.Remove(package);
            package.DestroyPackage();
            package.Status = Package.DeliveryStatus.DELIVERED;
        } else
        {
            package.Status = Package.DeliveryStatus.UNASSIGNED;
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
