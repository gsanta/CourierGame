
using System;

public class PlayerInputComponent
{
    private Player player;
    private InputHandler inputHandler;
    private PlayerStore playerStore;
    private DeliveryStore deliveryStore;
    private PackageStore packageStore;

    public PlayerInputComponent(DeliveryStore deliveryStore, PackageStore packageStore, InputHandler inputHandler, PlayerStore playerStore)
    {
        this.deliveryStore = deliveryStore;
        this.packageStore = packageStore;
        this.inputHandler = inputHandler;
        this.playerStore = playerStore;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void ActivateComponent()
    {
        inputHandler.OnKeyDown += OnKeyDown;
        inputHandler.OnLeftMouseButtonDown += OnLeftMouseButtonDown;
    }

    public void DeactivateComnponent()
    {
        inputHandler.OnKeyDown -= OnKeyDown;
        inputHandler.OnLeftMouseButtonDown -= OnLeftMouseButtonDown;
    }

    private void OnLeftMouseButtonDown(object sender, EventArgs e)
    {
        if (playerStore.IsActivePlayer(player))
        {
            Package deliveryPackage;
            if (deliveryStore.GetPackage(player, out deliveryPackage))
            {
                deliveryPackage.ReleasePackage();
            }
            else if (packageStore.GetPackageWithinPickupRange(player, out deliveryPackage))
            {
                deliveryPackage.PickupBy(player);
            }
        }
    }

    private void OnKeyDown(object sender, KeyDownEventArgs args)
    {
        if (playerStore.IsActivePlayer(player))
        {
            playerStore.SetNextPlayerAsActive();
        }
    }
}
