using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UI;

public class DeliveryPanel : MonoBehaviour
{
    [SerializeField] private DeliveryListItem deliveryListItemTemplate;
    private DeliveryStore deliveryStore;
    private PackageStore packageStore;
    private CourierService courierService;

    private List<DeliveryListItem> activeDeliveryItems = new List<DeliveryListItem>();

    [Inject]
    public void Construct(DeliveryStore deliveryStore, PackageStore packageStore, CourierService courierService)
    {
        this.deliveryStore = deliveryStore;
        this.packageStore = packageStore;
        this.courierService = courierService;
    }

    void Start()
    {
        packageStore.OnPackageAdded += HandlePackageAdded;
        courierService.CurrentRoleChanged += HandleCurrentRoleChanged;
        //packageStore.OnPackageAdded += RefreshWaitingDeliveryList;
        //deliveryService.OnDeliveryStatusChanged += RefreshWaitingDeliveryList;
    }

    private void HandlePackageAdded(object sender, PackageAddedEventArgs args)
    {
        Package package = args.Package;

        package.OnStatusChanged += HandlePackageStatusChanged;

        DeliveryListItem deliveryListItem = Instantiate(deliveryListItemTemplate, deliveryListItemTemplate.transform.parent);
        deliveryListItem.gameObject.SetActive(true);
        
        var controller = new DeliveryListItemController(deliveryListItem, courierService, deliveryStore);
        deliveryListItem.controller = controller;
        deliveryListItem.packageNameText.text = package.Name;
        controller.Package = package;
        activeDeliveryItems.Add(deliveryListItem);

    }

    private void HandlePackageStatusChanged(object sender, EventArgs e)
    {
        SetReservationEnabled();
    }

    private void HandleCurrentRoleChanged(object sender, EventArgs e)
    {
        SetReservationEnabled();
    }

    private void SetReservationEnabled()
    {
        var courier = courierService.FindPlayRole();
        bool isReservationEnabled = courier != null && courier.GetPackage() == null;
        activeDeliveryItems.ForEach(item => item.controller.SetReservationEnabled(isReservationEnabled));
    }
}
