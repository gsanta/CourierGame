using UnityEngine;
using Zenject;

namespace AI
{
    public class CourierAgent : GAgent, ICourier
    {
        [SerializeField]
        private Transform viewPoint;

        private Camera cam;
        private Package package;
        private string courierName;
        private bool isActive = false;

        public override void Start()
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

        protected override void LateUpdate()
        {
            base.LateUpdate();
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

        public void SetActive(bool isActive)
        {
            this.isActive = isActive;
        }

        public bool IsActive()
        {
            return isActive;
        }

        public class Factory : PlaceholderFactory<Object, CourierAgent>
        {
        }
    }
}
