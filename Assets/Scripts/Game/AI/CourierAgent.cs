using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI
{
    class CourierAgent : GAgent, ICourier
    {

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
    }
}
