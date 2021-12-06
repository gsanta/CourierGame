using GameObjects;
using Delivery;
using Game;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class DeliveryPanelController : MonoBehaviour, IDeliveryPanelController
    {
        [SerializeField]
        private DeliveryListItem deliveryListItemTemplate;

        private DeliveryService deliveryService;
        private DeliveryPanel deliveryPanel;

        [Inject]
        public void Construct(DeliveryPanel deliveryPanel, DeliveryService deliveryService)
        {
            this.deliveryPanel = deliveryPanel;
            this.deliveryService = deliveryService;
        }

        private void Awake()
        {
            deliveryPanel.SetDeliveryListItemInstantiator(this);
        }

        public IDeliveryListItem Instantiate(Package package)
        {
            return null;
        }


        public IEnumerator Destroy(int delay, IDeliveryListItem deliveryListItem)
        {
            yield return new WaitForSeconds(delay);
            Destroy(((DeliveryListItem) deliveryListItem).gameObject);
        }

    }
}
