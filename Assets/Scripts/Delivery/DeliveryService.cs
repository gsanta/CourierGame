

using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryService
{
    private List<DeliveryPackage> packages = new List<DeliveryPackage>();
    private Dictionary<Player, DeliveryPackage> packageMap = new Dictionary<Player, DeliveryPackage>();
    private Dictionary<DeliveryPackage, Player> reversePackageMap = new Dictionary<DeliveryPackage, Player>();

    public void AddPackage(DeliveryPackage package)
    {
        packages.Add(package);
        OnPackageAdded?.Invoke(this, EventArgs.Empty);
    }

    public void AssignPackageToPlayer(Player player, DeliveryPackage package)
    {
        packageMap.Add(player, package);
        reversePackageMap.Add(package, player);

        OnPackageAssigned?.Invoke(this, EventArgs.Empty);
    }

    public void DropPackage(DeliveryPackage package)
    {
        Player player;
        if (reversePackageMap.TryGetValue(package, out player))
        {
            packageMap.Remove(player);
        }
        reversePackageMap.Remove(package);

        if (Vector3.Distance(package.transform.position, package.targetObject.transform.position) < 1)
        {
            packages.Remove(package);
            package.DestroyPackage();
            OnPackageDelivered?.Invoke(this, EventArgs.Empty);
        } else
        {
            OnPackageAssigned?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool GetPackage(Player player, out DeliveryPackage package)
    {
        return packageMap.TryGetValue(player, out package);
    }

    public List<DeliveryPackage> AssignedPackages
    {
        get => packages.FindAll(package => GetPlayerForPackage(package) != null);
    }

    public List<DeliveryPackage> UnAssignedPackages
    {
        get => packages.FindAll(package => GetPlayerForPackage(package) == null);
    }

    public Player GetPlayerForPackage(DeliveryPackage package)
    {
        Player player;

        reversePackageMap.TryGetValue(package, out player);

        return player;
    }

    public event EventHandler OnPackageAdded;
    public event EventHandler OnPackageAssigned;
    public event EventHandler OnPackageDelivered;
}
