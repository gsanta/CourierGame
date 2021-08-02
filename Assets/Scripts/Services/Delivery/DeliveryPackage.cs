using UnityEngine;

public class DeliveryPackage : MonoBehaviour
{
    [HideInInspector] public PlayerService playerService;
    [HideInInspector] public DeliveryService deliveryService;

    private bool isHolding = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseDown();
        }

        if (isHolding)
        {
            PlayerController player = deliveryService.GetPlayerForPackage(this);

            //item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            gameObject.transform.SetParent(player.transform);
        }
        else
        {
            //objectPos = item.transform.position;
            //item.transform.SetParent(null);
            //item.GetComponent<Rigidbody>().useGravity = true;
            //item.transform.position = objectPos;
        }
    }

    //private void LateUpdate()
    //{
    //    if (deliveryService != null)
    //    {
    //        PlayerController playerController = deliveryService.GetPlayerForPackage(this);
    //        if (playerController != null)
    //        {
    //            transform.position = playerController.transform.position;
    //        }
    //    }
    //}

    private void HandleMouseDown()
    {
        if (deliveryService != null)
        {
            if (deliveryService.GetPlayerForPackage(this) != null)
            {
                PickupIfCloseToPlayer();
            }
        }
    }

    private void PickupIfCloseToPlayer()
    {
        PlayerController activePlayer = playerService.GetActivePlayer();

        if (Vector3.Distance(activePlayer.transform.position, transform.position) < 2)
        {
            isHolding = true;
            deliveryService.AssignPackageToPlayer(activePlayer, this);
        }
    }
}
