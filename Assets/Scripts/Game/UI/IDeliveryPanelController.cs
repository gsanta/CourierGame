
using Delivery;
using System.Collections;
using UI;
using UnityEngine;

namespace Game
{
    public interface IDeliveryPanelController
    {
        IDeliveryListItem Instantiate(Package package);
        IEnumerator Destroy(int delay, IDeliveryListItem deliveryListItem);
        Coroutine StartCoroutine(IEnumerator routine);
    }
}
