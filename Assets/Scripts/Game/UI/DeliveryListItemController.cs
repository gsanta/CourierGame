using Bikers;
using Delivery;

namespace UI
{
    public class DeliveryListItemController
    {
        private readonly IDeliveryListItem deliveryListItem;
        private readonly BikerService courierService;
        private readonly DeliveryService deliveryService;
        private Package package;
        private bool isReservationEnabled = false;

        public DeliveryListItemController(IDeliveryListItem deliveryListItem, BikerService courierService, DeliveryService deliveryService)
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
                deliveryListItem.GetPackagePrice().text = package.Price.ToString() + " $";

                UpdateReservationButtonStatus();
            }

            get => package;
        }

        private void UpdateReservationButtonStatus()
        {
            deliveryListItem.GetPackageStatus().text = package.Status.GetDescription();
            if (package.Status == DeliveryStatus.UNASSIGNED && isReservationEnabled)
            {
                deliveryListItem.GetPackageStatus().gameObject.SetActive(false);
                deliveryListItem.GetReservedButton().gameObject.SetActive(true);
            }
            else
            {
                deliveryListItem.GetPackageStatus().gameObject.SetActive(true);
                deliveryListItem.GetReservedButton().gameObject.SetActive(false);
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
