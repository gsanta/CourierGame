using Bikers;
using Delivery;
using Game;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class DeliveryPanelController : MonoBehaviour, IDeliveryListItemInstantiator
    {
        [SerializeField]
        private DeliveryListItem deliveryListItemTemplate;

        private BikerService bikerService;
        private DeliveryService deliveryService;

        [Inject]
        public void Construct(BikerService bikerService, DeliveryService deliveryService)
        {
            this.bikerService = bikerService;
            this.deliveryService = deliveryService;
        }

        public IDeliveryListItem Instantiate(Package package)
        {
            DeliveryListItem deliveryListItem = Instantiate(deliveryListItemTemplate, deliveryListItemTemplate.transform.parent);
            deliveryListItem.gameObject.SetActive(true);

            var controller = new DeliveryListItemController(deliveryListItem, bikerService, deliveryService);
            deliveryListItem.controller = controller;
            deliveryListItem.packageName.text = package.Name;
            controller.Package = package;


            return deliveryListItem;
        }

        private IEnumerator RemovePackageAfterTimeout(int delay, Package package, List<IDeliveryListItem> deliveryListItems)
        {
            yield return new WaitForSeconds(delay);
            var item = deliveryListItems.Find(item => item.Package == package);
            deliveryListItems.Remove(item);
            item.gameObject.SetActive(false);
            Destroy(item.gameObject);
        }

    }
}
