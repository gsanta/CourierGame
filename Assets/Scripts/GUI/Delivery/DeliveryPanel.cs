using Bikers;
using Scenes;
using Delivery;
using Game;
using Model;
using Service;
using System;
using System.Collections.Generic;
using Zenject;

namespace UI
{
    public class DeliveryPanel : IResetable
    {
        private PackageStore packageStore;
        private IDeliveryPanelController deliveryListItemInstantiator;

        private List<IDeliveryListItem> deliveryListItems = new List<IDeliveryListItem>();

        [Inject]
        public void Construct(PackageStore packageStore, EventService eventService)
        {
            this.packageStore = packageStore;

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

            if (e.Package.Status == DeliveryStatus.DELIVERED)
            {
                var item = deliveryListItems.Find(item => item.GetController().Package == e.Package);
                deliveryListItems.Remove(item);
                deliveryListItemInstantiator.StartCoroutine(deliveryListItemInstantiator.Destroy(2, item));
            }
        }

        private void HandleCurrentRoleChanged(object sender, EventArgs e)
        {
        }

        public void Reset()
        {
            deliveryListItems.ForEach(item => item.Destroy());
            deliveryListItems.Clear();
        }
    }

}
 