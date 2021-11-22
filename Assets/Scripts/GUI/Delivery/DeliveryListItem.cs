using System;
using TMPro;
using UI;
using UnityEngine;

public class DeliveryListItem : MonoBehaviour, IDeliveryListItem
{
    [SerializeField]
    private TMP_Text packageName;
    [SerializeField]
    private TMP_Text playerName;
    [SerializeField]
    private TMP_Text packagePrice;
    [SerializeField]
    private TMP_Text packageStatus;
    [SerializeField]
    private GameObject reserveButton;

    private DeliveryListItemController controller;

    public TMP_Text GetPackageName()
    {
        return packageName;
    }

    public TMP_Text GetPlayerName()
    {
        return playerName;
    }

    public TMP_Text GetPackagePrice()
    {
        return packagePrice;
    }

    public TMP_Text GetPackageStatus()
    {
        return packageStatus;
    }

    public GameObject GetReservedButton()
    {
        return reserveButton;
    }

    public void SetController(DeliveryListItemController controller)
    {
        this.controller = controller;
    }

    public DeliveryListItemController GetController()
    {
        return controller;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public event EventHandler OnReserveButtonClick;
}
