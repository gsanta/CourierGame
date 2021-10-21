using Bikers;
using Delivery;
using Game;
using Model;
using Service;
using System;
using System.Collections.Generic;
using Zenject;

namespace UI
{
    public class DeliveryPanel
    {
        private PackageStore packageStore;
        private BikerService bikerService;
        private EventService eventService;
        private IDeliveryPanelController deliveryListItemInstantiator;

        private List<IDeliveryListItem> deliveryListItems = new List<IDeliveryListItem>();

        [Inject]
        public void Construct(PackageStore packageStore, BikerService bikerService, EventService eventService)
        {
            this.packageStore = packageStore;
            this.bikerService = bikerService;
            this.eventService = eventService;

            packageStore.OnPackageAdded += HandlePackageAdded;
            eventService.BikerCurrentRoleChanged += HandleCurrentRoleChanged;
            eventService.PackageStatusChanged += HandlePackageStatusChanged;
        }

        public void SetDeliveryListItemInstantiator(IDeliveryPanelController deliveryListItemInstantiator)
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
                var item = deliveryListItems.Find(item => item.GetController().Package == e.Package);
                deliveryListItems.Remove(item);
                deliveryListItemInstantiator.StartCoroutine(deliveryListItemInstantiator.Destroy(2, item));
            }
        }

        private void HandleCurrentRoleChanged(object sender, EventArgs e)
        {
            SetReservationEnabled();
        }

        private void SetReservationEnabled()
        {
            var courier = bikerService.FindPlayRole();
            bool isReservationEnabled = courier != null && courier.GetPackage() == null;
            deliveryListItems.ForEach(item => item.GetController().SetReservationEnabled(isReservationEnabled));
        }
    }

}
 