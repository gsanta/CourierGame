using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UI;
using System.Collections;
using Service;
using Domain;

namespace UI
{
    public class DeliveryPanel : MonoBehaviour
    {
        [SerializeField] private DeliveryListItem deliveryListItemTemplate;
        private PackageStore packageStore;
        private BikerService bikerService;
        private IEventService eventService;
        private IDeliveryService deliveryService;

        private List<DeliveryListItem> activeDeliveryItems = new List<DeliveryListItem>();

        [Inject]
        public void Construct(IDeliveryService deliveryService, PackageStore packageStore, BikerService bikerService, IEventService eventService)
        {
            this.deliveryService = deliveryService;
            this.packageStore = packageStore;
            this.bikerService = bikerService;
            this.eventService = eventService;
        }

        void Start()
        {
            packageStore.OnPackageAdded += HandlePackageAdded;
            eventService.BikerCurrentRoleChanged += HandleCurrentRoleChanged;
            eventService.PackageStatusChanged += HandlePackageStatusChanged;
        }

        private void HandlePackageAdded(object sender, PackageAddedEventArgs args)
        {
            Package package = args.Package;

            DeliveryListItem deliveryListItem = Instantiate(deliveryListItemTemplate, deliveryListItemTemplate.transform.parent);
            deliveryListItem.gameObject.SetActive(true);
        
            var controller = new DeliveryListItemController(deliveryListItem, bikerService, deliveryService);
            deliveryListItem.controller = controller;
            deliveryListItem.packageName.text = package.Name;
            controller.Package = package;
            activeDeliveryItems.Add(deliveryListItem);

        }

        private void HandlePackageStatusChanged(object sender, PackageStatusChangedEventArgs e)
        {
            SetReservationEnabled();

            if (e.Package.Status == DeliveryStatus.DELIVERED)
            {
                StartCoroutine(RemovePackageAfterTimeout(2, e.Package));
            }
        }

        private void HandleCurrentRoleChanged(object sender, EventArgs e)
        {
            SetReservationEnabled();
        }

        private IEnumerator RemovePackageAfterTimeout(int delay, Package package)
        {
            yield return new WaitForSeconds(delay);
            var item = activeDeliveryItems.Find(item => item.controller.Package == package);
            activeDeliveryItems.Remove(item);
            item.gameObject.SetActive(false);
            Destroy(item.gameObject);
        }

        private void SetReservationEnabled()
        {
            var courier = bikerService.FindPlayRole();
            bool isReservationEnabled = courier != null && courier.GetPackage() == null;
            activeDeliveryItems.ForEach(item => item.controller.SetReservationEnabled(isReservationEnabled));
        }
    }

}
