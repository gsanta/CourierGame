using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class DeliveryListItemController
    {
        private readonly DeliveryListItem deliveryListItem;
        private readonly CourierService courierService;
        private readonly DeliveryStore deliveryStore;
        private Package package;

        public DeliveryListItemController(DeliveryListItem deliveryListItem, CourierService courierService, DeliveryStore deliveryStore)
        {
            this.deliveryListItem = deliveryListItem;
            this.courierService = courierService;
            this.deliveryStore = deliveryStore;
            deliveryListItem.OnReserveButtonClick += HandleReserveButtonClick;
        }

        public Package Package
        {
            set
            {
                package = value;

                UpdateStatus();
            }

            get => package;
        }

        public void UpdateStatus()
        {
            deliveryListItem.packageStatus.text = package.Status.GetDescription();
            if (package.Status == DeliveryStatus.UNASSIGNED)
            {
                deliveryListItem.packageStatus.gameObject.SetActive(false);
                deliveryListItem.reserveButton.gameObject.SetActive(true);
            }
            else
            {
                deliveryListItem.packageStatus.gameObject.SetActive(true);
                deliveryListItem.reserveButton.gameObject.SetActive(false);
            }
        }


        private void HandleReserveButtonClick(object sender, EventArgs e)
        {
            var player = courierService.FindPlayRole();
            if (player != null && player.GetPackage() == null)
            {
                deliveryStore.AssignPackageToPlayer(player, package);
            }
        }
    }
}
