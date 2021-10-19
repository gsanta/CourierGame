using Bikers;
using Delivery;
using Game;
using Model;
using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UI
{
    public class DeliveryPanel
    {
        private PackageStore packageStore;
        private BikerService bikerService;
        private EventService eventService;
        private IDeliveryService deliveryService;
        private IDeliveryListItemInstantiator deliveryListItemInstantiator;

        private List<IDeliveryListItem> deliveryListItems = new List<IDeliveryListItem>();

        [Inject]
        public void Construct(IDeliveryService deliveryService, PackageStore packageStore, BikerService bikerService, EventService eventService)
        {
            this.deliveryService = deliveryService;
            this.packageStore = packageStore;
            this.bikerService = bikerService;
            this.eventService = eventService;
        }

        public void SetDeliveryListItemInstantiator(IDeliveryListItemInstantiator deliveryListItemInstantiator)
        {
            this.deliveryListItemInstantiator = deliveryListItemInstantiator;
            ClearDeliveryListItems();
            packageStore.GetAll().ForEach(package => AddPackage(package));
        }

        private void AddPackage(Package package)
        {
            if (deliveryListItemInstantiator != null)
            {
                CreateDeliveryListItem(package);
            }
        }

        private void CreateDeliveryListItem(Package package)
        {
            IDeliveryListItem deliveryListItem = deliveryListItemInstantiator.Instantiate(package);

            deliveryListItems.Add(deliveryListItem);
        }

        private void ClearDeliveryListItems()
        {
            deliveryListItems.ForEach(item => item.Destroy());
            deliveryListItems.Clear();
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
            AddPackage(package);
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
            var item = deliveryListItems.Find(item => item.controller.Package == package);
            deliveryListItems.Remove(item);
            item.gameObject.SetActive(false);
            Destroy(item.gameObject);
        }

        private void SetReservationEnabled()
        {
            var courier = bikerService.FindPlayRole();
            bool isReservationEnabled = courier != null && courier.GetPackage() == null;
            deliveryListItems.ForEach(item => item.controller.SetReservationEnabled(isReservationEnabled));
        }
    }

}
 