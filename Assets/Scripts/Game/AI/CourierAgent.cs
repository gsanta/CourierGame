using UnityEngine;
using Zenject;

namespace AI
{
    public class CourierAgent : GAgent, ICourier
    {
        private Package package;

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
            return agentId;
        }

        public void SetPackage(Package package)
        {
            this.package = package;
        }

        public Package GetPackage()
        {
            return package;
        }

        public class Factory : PlaceholderFactory<Object, CourierAgent>
        {
        }
    }
}
