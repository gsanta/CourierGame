using System;
using UnityEngine;
using Zenject;

namespace Model
{
    public class BikerPlayComponent : MonoBehaviour
    {
        [SerializeField]
        private CharacterController charController;
        [SerializeField]
        private float moveSpeed = 5f, runSpeed = 8f;
        [SerializeField]
        private float gravityMod = 2.5f;

        private InputHandler inputHandler;
        private PackageStore packageStore;
        private IDeliveryService deliveryService;

        private float activeMoveSpeed;
        private Vector3 moveDir, movement;
        private bool isActivated = false;

        public void Construct(PackageStore packageStore, InputHandler inputHandler, IDeliveryService deliveryService)
        {
            this.packageStore = packageStore;
            this.inputHandler = inputHandler;
            this.deliveryService = deliveryService;
        }

        private Biker Biker { get => GetComponentInParent<Biker>(); }

        public void SetActivated(bool isActivated)
        {
            if (this.isActivated != isActivated)
            {
                this.isActivated = isActivated;

                if (isActivated)
                {
                    inputHandler.OnKeyDown += OnKeyDown;
                }
                else
                {
                    inputHandler.OnKeyDown -= OnKeyDown;
                }

            }
        }

        private void Update()
        {
            if (isActivated)
            {
                Move();
            }
        }

        private void OnKeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.Key == "e")
            {
                if (Biker.GetPackage() == null)
                {
                    return;
                }

                Package deliveryPackage;
                if (Biker.GetPackage().Status == DeliveryStatus.RESERVED)
                {
                    deliveryService.AssignPackage(Biker.GetPackage());
                }
                else if (Biker.GetPackage().Status == DeliveryStatus.ASSIGNED)
                //else if (packageStore.GetPackageWithinPickupRange(Biker, out deliveryPackage))
                {
                    deliveryService.DeliverPackage(Biker.GetPackage(), true);
                }
            }
        }

        private void Move()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + horizontal, transform.rotation.eulerAngles.z);

            moveDir = new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));

            if (Input.GetKey(KeyCode.LeftShift))
            {
                activeMoveSpeed = runSpeed;
            }
            else
            {
                activeMoveSpeed = moveSpeed;
            }

            movement = (transform.forward * moveDir.z + transform.right * moveDir.x).normalized * activeMoveSpeed;
            movement.y += Physics.gravity.y * gravityMod;
            charController.Move(movement * Time.deltaTime);
        }

        public class Factory : PlaceholderFactory<UnityEngine.Object, BikerPlayComponent>
        {
        }
    }
}