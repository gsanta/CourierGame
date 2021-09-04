using System;
using TMPro;
using UI;
using UnityEngine;

public class DeliveryListItem : MonoBehaviour
{
    [SerializeField]
    public TMP_Text packageName;
    [SerializeField]
    public TMP_Text playerName;
    [SerializeField]
    public TMP_Text packagePrice;
    [SerializeField]
    public TMP_Text packageStatus;
    [SerializeField]
    public GameObject reserveButton;

    public DeliveryListItemController controller;

    public void HandleReserveButtonClick()
    {
        OnReserveButtonClick?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler OnReserveButtonClick;
}
