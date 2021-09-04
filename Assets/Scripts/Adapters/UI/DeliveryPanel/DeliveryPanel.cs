using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UI;
using System.Collections;

namespace UI
{
    public class DeliveryPanel : MonoBehaviour
    {
        [SerializeField] private DeliveryListItem deliveryListItemTemplate;
        private DeliveryStore deliveryStore;
        private PackageStore packageStore;
        private BikerService courierService;

        private List<DeliveryListItem> activeDeliveryItems = new List<DeliveryListItem>();

        [Inject]
        public void Construct(DeliveryStore deliveryStore, PackageStore packageStore, BikerService courierService)
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

            package.StatusChanged += HandlePackageStatusChanged;

            DeliveryListItem deliveryListItem = Instantiate(deliveryListItemTemplate, deliveryListItemTemplate.transform.parent);
            deliveryListItem.gameObject.SetActive(true);
        
            var controller = new DeliveryListItemController(deliveryListItem, courierService);
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
            var courier = courierService.FindPlayRole();
            bool isReservationEnabled = courier != null && courier.GetPackage() == null;
            activeDeliveryItems.ForEach(item => item.controller.SetReservationEnabled(isReservationEnabled));
        }
    }

}
