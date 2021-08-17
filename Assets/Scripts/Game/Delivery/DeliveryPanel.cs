using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DeliveryPanel : MonoBehaviour
{
    [SerializeField] private DeliveryListItem deliveryListItemTemplate;
    [SerializeField] private WaitingDeliveryListItem waitingDeliveryListItemTemplate;
    private DeliveryStore deliveryService;
    private PackageStore packageStore;

    private List<DeliveryListItem> activeDeliveryItems = new List<DeliveryListItem>();
    private List<WaitingDeliveryListItem> waitingDeliveryItems = new List<WaitingDeliveryListItem>();

    [Inject]
    public void Construct(DeliveryStore deliveryService, PackageStore packageStore)
    {
        this.deliveryService = deliveryService;
        this.packageStore = packageStore;
    }

    void Start()
    {
        packageStore.OnPackageAdded += HandlePackageAdded;
        deliveryService.OnDeliveryStatusChanged += RefreshActiveDeliveryList;

        //packageStore.OnPackageAdded += RefreshWaitingDeliveryList;
        //deliveryService.OnDeliveryStatusChanged += RefreshWaitingDeliveryList;
    }

    private void HandlePackageAdded(object sender, PackageAddedEventArgs args)
    {
        Package package = args.Package;

        package.OnStatusChanged += HandlePackageStatusChanged;

        DeliveryListItem deliveryListItem = Instantiate(deliveryListItemTemplate, deliveryListItemTemplate.transform.parent);
        deliveryListItem.packageNameText.text = package.Name;
        //deliveryListItem.playerNameText.text = packagedeliveryService.GetPlayerForPackage(package).GetName();
        deliveryListItem.packageStatus.text = package.Status.GetDescription();
        deliveryListItem.package = package;
        deliveryListItem.gameObject.SetActive(true);
        activeDeliveryItems.Add(deliveryListItem);

    }

    private void HandlePackageStatusChanged(object sender, EventArgs e)
    {
        Package package = (Package)sender;
        DeliveryListItem deliveryListItem = activeDeliveryItems.Find(item => item.package == package);
        deliveryListItem.packageStatus.text = package.Status.GetDescription();
    }

    private void RefreshActiveDeliveryList(object sender, EventArgs e)
    {
        foreach (DeliveryListItem item in activeDeliveryItems)
        {
            Destroy(item.gameObject);
        }

        activeDeliveryItems.Clear();

        foreach (Package package in packageStore.GetAll())
        {
            DeliveryListItem deliveryListItem = Instantiate(deliveryListItemTemplate, deliveryListItemTemplate.transform.parent);
            deliveryListItem.packageNameText.text = package.Name;
            //deliveryListItem.playerNameText.text = packagedeliveryService.GetPlayerForPackage(package).GetName();
            deliveryListItem.packageStatus.text = package.Status.GetDescription();
            deliveryListItem.gameObject.SetActive(true);
            activeDeliveryItems.Add(deliveryListItem);
        }
    }

    private void RefreshWaitingDeliveryList(object sender, EventArgs e)
    {
        foreach (WaitingDeliveryListItem item in waitingDeliveryItems)
        {
            Destroy(item.gameObject);
        }

        waitingDeliveryItems.Clear();

        foreach (Package package in deliveryService.UnAssignedPackages)
        {
            WaitingDeliveryListItem deliveryListItem = Instantiate(waitingDeliveryListItemTemplate, waitingDeliveryListItemTemplate.transform.parent);
            deliveryListItem.packageNameText.text = package.Name;
            deliveryListItem.gameObject.SetActive(true);
            waitingDeliveryItems.Add(deliveryListItem);
        }
    }
}
