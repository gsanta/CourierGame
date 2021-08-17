using UnityEngine;
using TMPro;

public class DeliveryListItem : MonoBehaviour
{
    [SerializeField] public TMP_Text packageNameText;
    [SerializeField] public TMP_Text playerNameText;
    [SerializeField] public TMP_Text packageStatus;

    public Package package;
}
