using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPanel : MonoBehaviour
{
    [SerializeField] private GameObject deliveryListItemTemplate;
    [SerializeField] private GameObject waitingDeliveryStatusRow;
    [HideInInspector] public DeliveryService deliveryService;

    private List<GameObject> activeDeliveryItems = new List<GameObject>();

    void Start()
    {
        deliveryService.OnPackageAdded += RefreshActiveDeliveryList;
        deliveryService.OnPackageAssigned += RefreshActiveDeliveryList;
        deliveryService.OnPackageDelivered += RefreshActiveDeliveryList;
    }

    private void RefreshActiveDeliveryList(object sender, EventArgs e)
    {
        activeDeliveryItems.Clear();

        foreach (DeliveryPackage package in deliveryService.AssignedPackages)
        {
            GameObject deliveryListItem = Instantiate(deliveryListItemTemplate, deliveryListItemTemplate.transform.parent);
        }
    }
}
