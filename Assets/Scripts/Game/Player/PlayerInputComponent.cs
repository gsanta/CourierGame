
using System;
using UnityEngine;

public class PlayerInputComponent
{
    private ICourier courier;
    private InputHandler inputHandler;
    private DeliveryStore deliveryStore;
    private PackageStore packageStore;

    public PlayerInputComponent(DeliveryStore deliveryStore, PackageStore packageStore, InputHandler inputHandler)
    {
        this.deliveryStore = deliveryStore;
        this.packageStore = packageStore;
        this.inputHandler = inputHandler;
    }

    public void SetPlayer(ICourier courier)
    {
        this.courier = courier;
    }

    public void ActivateComponent()
    {
        inputHandler.OnKeyDown += OnKeyDown;
    }

    public void DeactivateComnponent()
    {
        inputHandler.OnKeyDown -= OnKeyDown;
    }

    private void OnKeyDown(object sender, KeyDownEventArgs e)
    {
        if (e.Key == "e")
        {
            Package deliveryPackage;
            if (courier.GetPackage())
            {
                courier.GetPackage().DeliverPackage();
            }
            else if (packageStore.GetPackageWithinPickupRange(courier, out deliveryPackage))
            {
                deliveryPackage.PickupBy(courier);
            }
        }
    }
}
