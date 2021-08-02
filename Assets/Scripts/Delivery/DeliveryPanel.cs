using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPanel : MonoBehaviour
{
    [SerializeField] private DeliveryListItem deliveryListItemTemplate;
    [SerializeField] private WaitingDeliveryListItem waitingDeliveryListItemTemplate;
    [HideInInspector] public DeliveryService deliveryService;

    private List<DeliveryListItem> activeDeliveryItems = new List<DeliveryListItem>();
    private List<WaitingDeliveryListItem> waitingDeliveryItems = new List<WaitingDeliveryListItem>();

    void Start()
    {
        deliveryService.OnPackageAdded += RefreshActiveDeliveryList;
        deliveryService.OnPackageAssigned += RefreshActiveDeliveryList;
        deliveryService.OnPackageDelivered += RefreshActiveDeliveryList;

        deliveryService.OnPackageAdded += RefreshWaitingDeliveryList;
        deliveryService.OnPackageAssigned += RefreshWaitingDeliveryList;
        deliveryService.OnPackageDelivered += RefreshWaitingDeliveryList;
    }

    private void RefreshActiveDeliveryList(object sender, EventArgs e)
    {
        foreach (DeliveryListItem item in activeDeliveryItems)
        {
            Destroy(item.gameObject);
        }

        activeDeliveryItems.Clear();

        foreach (DeliveryPackage package in deliveryService.AssignedPackages)
        {
            DeliveryListItem deliveryListItem = Instantiate(deliveryListItemTemplate, deliveryListItemTemplate.transform.parent);
            deliveryListItem.packageNameText.text = package.Name;
            deliveryListItem.playerNameText.text = deliveryService.GetPlayerForPackage(package).Name;
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

        foreach (DeliveryPackage package in deliveryService.UnAssignedPackages)
        {
            WaitingDeliveryListItem deliveryListItem = Instantiate(waitingDeliveryListItemTemplate, waitingDeliveryListItemTemplate.transform.parent);
            deliveryListItem.packageNameText.text = package.Name;
            deliveryListItem.gameObject.SetActive(true);
            waitingDeliveryItems.Add(deliveryListItem);
        }
    }
}
