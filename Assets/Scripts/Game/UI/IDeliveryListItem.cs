
using TMPro;
using UnityEngine;

namespace UI
{
    public interface IDeliveryListItem
    {
        void Destroy();

        void SetDeliveryListItemController(DeliveryListItemController controller);
        DeliveryListItemController GetDeliveryListItemController();

        TMP_Text GetPackageName();
        TMP_Text GetPlayerName();
        TMP_Text GetPackagePrice();
        TMP_Text GetPackageStatus();
        GameObject GetReservedButton();
    }
}
