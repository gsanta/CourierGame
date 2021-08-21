﻿using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace AI
{
    public class CourierAgent : GAgent, ICourier
    {
        [SerializeField]
        private Transform viewPoint;
        [SerializeField]
        private CharacterController charController;
        [SerializeField]
        private float moveSpeed = 5f, runSpeed = 8f;
        [SerializeField]
        private float gravityMod = 2.5f;

        private Camera cam;
        private Package package;
        private string courierName;
        private bool isActive = false;
        private bool isPlayer = false;

        private float activeMoveSpeed;
        private Vector3 moveDir, movement;

        private PlayerInputComponent playerInputComponent;

        [Inject]
        public void Construct(PlayerInputComponent playerInputComponent, PackageStore packageStore)
        {
            this.playerInputComponent = playerInputComponent;
            playerInputComponent.SetPlayer(this);

            actions.Add(new AssignPackageAction(this, packageStore));
            actions.Add(new DeliverPackageAction(this));
            actions.Add(new PickUpPackageAction(this));
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

            movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * activeMoveSpeed;
            movement.y += Physics.gravity.y * gravityMod;
            charController.Move(movement * Time.deltaTime);
            Debug.Log("y: " + transform.position.y);
        }

        public void ActivatePlayer()
        {
            if (playerInputComponent != null)
            {
                playerInputComponent.ActivateComponent();
            }
        }

        public void DeactivatePlayer()
        {
            playerInputComponent.DeactivateComnponent();
        }

        protected override void Start()
        {
            base.Start();
            cam = Camera.main;
        }

        //public new void Start()
        //{
        //    base.Start();
        //    SubGoal s1 = new SubGoal("isPackageDropped", 1, true);
        //    goals.Add(s1, 3);
        //}

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public string GetId()
        {
            return agentId;
        }

        public string GetName()
        {
            return courierName;
        }

        public void SetName(string name)
        {
            this.courierName = name;
        }

        public void SetPackage(Package package)
        {
            this.package = package;
        }

        public Package GetPackage()
        {
            return package;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        protected void Update()
        {
            if (isPlayer)
            {
                Move();
            }
        }

        protected override void LateUpdate()
        {
            if (!isPlayer)
            {
                base.LateUpdate();
            }
            SetCameraPosition();
        }

        private void SetCameraPosition()
        {
            if (isActive)
            {
                cam.transform.position = viewPoint.position;
                cam.transform.rotation = viewPoint.rotation;
            }
        }

        public void SetFollow(bool isActive)
        {
            this.isActive = isActive;
        }

        public bool IsFollow()
        {
            return isActive;
        }

        public void SetPlayer(bool isPlayer)
        {
            if (this.isPlayer != isPlayer)
            {
                this.isPlayer = isPlayer;
                gameObject.GetComponent<NavMeshAgent>().enabled = !isPlayer;
                
                if (isPlayer)
                {
                    SetRunning(false);
                } else
                {
                    SetRunning(true);
                }
            }
        }

        public bool IsPlayer()
        {
            return isPlayer;
        }

        protected override WorldStates GetWorldStates()
        {
            //var package = GetPackage();
            //if (package)
            //{
            //    switch (package.Status)
            //    {
            //        case DeliveryStatus.ASSIGNED:
            //    }
            //    if (package.Status)
            //}
            return null;
        }

        public class Factory : PlaceholderFactory<Object, CourierAgent>
        {
        }
    }
}
