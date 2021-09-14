using Model;
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
        private readonly IDeliveryService deliveryService;
        private Package package;
        private bool isReservationEnabled = false;

        public DeliveryListItemController(DeliveryListItem deliveryListItem, BikerService courierService, IDeliveryService deliveryService)
        {
            this.deliveryListItem = deliveryListItem;
            this.courierService = courierService;
            this.deliveryService = deliveryService;
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

        public void HandleReserveButtonClick()
        {
            var biker = courierService.FindPlayRole();
            if (biker != null && biker.GetPackage() == null)
            {
                deliveryService.ReservePackage(package, biker);
            }
        }
    }
}
