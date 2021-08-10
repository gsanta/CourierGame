using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI
{

    public class SubGoal {
        public Dictionary<string, int> sgoals;
        public bool remove;
    
        public SubGoal(string sgoal, int i, bool remove)
        {
            sgoals = new Dictionary<string, int>();
            sgoals.Add(sgoal, i);
            this.remove = remove;
        }
    }

    public class GAgent : MonoBehaviour
    {
        public List<GAction> actions = new List<GAction>();
        public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

        public GAction currentAction;
        private GPlanner planner;
        private Queue<GAction> actionQueue;
        private SubGoal currentGoal;

        void Start()
        {
            GAction[] acts = GetComponents<GAction>();
            foreach (GAction a in acts)
            {
                actions.Add(a);
            }
        }

        void LateUpdate()
        {

        }
    }
}
