using UnityEngine;

namespace AI
{
    class CourierAgent : GAgent, ICourier
    {
        private Package package;

        public new void Start()
        {
            base.Start();
            SubGoal s1 = new SubGoal("isPackagePickedUp", 1, true);
            goals.Add(s1, 3);
        }

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
    }
}
