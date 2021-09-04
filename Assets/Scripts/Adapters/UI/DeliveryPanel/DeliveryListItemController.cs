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
        private readonly BikerService courierService;
        private Package package;
        private bool isReservationEnabled = false;

        public DeliveryListItemController(DeliveryListItem deliveryListItem, BikerService courierService)
        {
            this.deliveryListItem = deliveryListItem;
            this.courierService = courierService;
            deliveryListItem.OnReserveButtonClick += HandleReserveButtonClick;
        }

        public void SetReservationEnabled(bool isEnabled)
        {
            isReservationEnabled = isEnabled;
            UpdateReservationButtonStatus();
        }

        public Package Package
        {
            set
            {
                package = value;
                deliveryListItem.packagePrice.text = package.Price.ToString() + " $";

                UpdateReservationButtonStatus();
            }

            get => package;
        }

        private void UpdateReservationButtonStatus()
        {
            deliveryListItem.packageStatus.text = package.Status.GetDescription();
            if (package.Status == DeliveryStatus.UNASSIGNED && isReservationEnabled)
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
                package.ReservePackage(player);
            }
        }
    }
}
