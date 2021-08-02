

using System.Collections.Generic;

public class DeliveryService
{
    private Dictionary<PlayerController, DeliveryPackage> packageMap = new Dictionary<PlayerController, DeliveryPackage>();
    private Dictionary<DeliveryPackage, PlayerController> reversePackageMap = new Dictionary<DeliveryPackage, PlayerController>();

    public void AssignPackageToPlayer(PlayerController player, DeliveryPackage package)
    {
        packageMap.Add(player, package);
        reversePackageMap.Add(package, player);


    }

    public PlayerController GetPlayerForPackage(DeliveryPackage package)
    {
        PlayerController player;

        reversePackageMap.TryGetValue(package, out player);

        return player;
    }
}
