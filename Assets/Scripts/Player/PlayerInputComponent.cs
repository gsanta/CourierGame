
using System;

public class PlayerInputComponent
{
    private Player player;
    private InputHandler inputHandler;
    private PlayerStore playerStore;
    private DeliveryStore deliveryService;
    private PackageStore packageStore;

    public PlayerInputComponent(Player player, DeliveryStore deliveryService, PackageStore packageStore, InputHandler inputHandler, PlayerStore playerStore)
    {
        this.player = player;
        this.deliveryService = deliveryService;
        this.packageStore = packageStore;
        this.inputHandler = inputHandler;
        this.playerStore = playerStore;
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
        Package deliveryPackage;
        if (deliveryService.GetPackage(player, out deliveryPackage))
        {
            deliveryPackage.ReleasePackage();
        }
        else if (packageStore.GetPackageWithinPickupRange(player, out deliveryPackage))
        {
            deliveryPackage.PickupBy(player);
        }
    }

    private void OnKeyDown(object sender, KeyDownEventArgs args)
    {
        playerStore.SetNextPlayerAsActive();
    }
}
