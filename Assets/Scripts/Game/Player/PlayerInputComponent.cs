
using System;

public class PlayerInputComponent
{
    private ICourier courier;
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

    public void SetPlayer(ICourier courier)
    {
        this.courier = courier;
    }

    public void ActivateComponent()
    {
        inputHandler.OnLeftMouseButtonDown += OnLeftMouseButtonDown;
    }

    public void DeactivateComnponent()
    {
        inputHandler.OnLeftMouseButtonDown -= OnLeftMouseButtonDown;
    }

    private void OnLeftMouseButtonDown(object sender, EventArgs e)
    {
        Package deliveryPackage;
        if (deliveryStore.GetPackage(courier, out deliveryPackage))
        {
            //deliveryPackage.ReleasePackage();
        }
        else if (packageStore.GetPackageWithinPickupRange(courier, out deliveryPackage))
        {
            deliveryPackage.PickupBy(courier);
        }
    }
}
