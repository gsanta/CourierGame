

using System;
using System.Collections.Generic;

public class DeliveryService
{
    private List<DeliveryPackage> packages = new List<DeliveryPackage>();
    private Dictionary<PlayerController, DeliveryPackage> packageMap = new Dictionary<PlayerController, DeliveryPackage>();
    private Dictionary<DeliveryPackage, PlayerController> reversePackageMap = new Dictionary<DeliveryPackage, PlayerController>();

    public void AddPackage(DeliveryPackage package)
    {
        packages.Add(package);
        OnPackageAdded?.Invoke(this, EventArgs.Empty);
    }

    public void AssignPackageToPlayer(PlayerController player, DeliveryPackage package)
    {
        packageMap.Add(player, package);
        reversePackageMap.Add(package, player);

        OnPackageAssigned?.Invoke(this, EventArgs.Empty);
    }

    public void RemovePackageFromPlayer(DeliveryPackage package)
    {
        PlayerController player;
        if (reversePackageMap.TryGetValue(package, out player))
        {
            packageMap.Remove(player);
        }
        reversePackageMap.Remove(package);
        packages.Remove(package);
        OnPackageDelivered?.Invoke(this, EventArgs.Empty);
    }

    public bool GetPackage(PlayerController player, out DeliveryPackage package)
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

    public PlayerController GetPlayerForPackage(DeliveryPackage package)
    {
        PlayerController player;

        reversePackageMap.TryGetValue(package, out player);

        return player;
    }

    public event EventHandler OnPackageAdded;
    public event EventHandler OnPackageAssigned;
    public event EventHandler OnPackageDelivered;
}
