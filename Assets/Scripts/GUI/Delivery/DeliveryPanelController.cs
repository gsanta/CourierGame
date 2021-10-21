using Bikers;
using Delivery;
using Game;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class DeliveryPanelController : MonoBehaviour, IDeliveryPanelController
    {
        [SerializeField]
        private DeliveryListItem deliveryListItemTemplate;

        private BikerService bikerService;
        private DeliveryService deliveryService;
        private DeliveryPanel deliveryPanel;

        [Inject]
        public void Construct(DeliveryPanel deliveryPanel, BikerService bikerService, DeliveryService deliveryService)
        {
            this.deliveryPanel = deliveryPanel;
            this.bikerService = bikerService;
            this.deliveryService = deliveryService;
        }

        private void Awake()
        {
            deliveryPanel.SetDeliveryListItemInstantiator(this);
        }

        public IDeliveryListItem Instantiate(Package package)
        {
            DeliveryListItem deliveryListItem = Instantiate(deliveryListItemTemplate, deliveryListItemTemplate.transform.parent);
            deliveryListItem.gameObject.SetActive(true);

            var controller = new DeliveryListItemController(deliveryListItem, bikerService, deliveryService);
            deliveryListItem.SetController(controller);
            deliveryListItem.GetPackageName().text = package.Name;
            controller.Package = package;

            return deliveryListItem;
        }


        public IEnumerator Destroy(int delay, IDeliveryListItem deliveryListItem)
        {
            yield return new WaitForSeconds(delay);
            Destroy(((DeliveryListItem) deliveryListItem).gameObject);
        }

    }
}
