
using TMPro;
using UnityEngine;

namespace UI
{
    public interface IDeliveryListItem
    {
        void Destroy();

        void SetController(DeliveryListItemController controller);
        DeliveryListItemController GetController();

        TMP_Text GetPackageName();
        TMP_Text GetPlayerName();
        TMP_Text GetPackagePrice();
        TMP_Text GetPackageStatus();
        GameObject GetReservedButton();
    }
}
