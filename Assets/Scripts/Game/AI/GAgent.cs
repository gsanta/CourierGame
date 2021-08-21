using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

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

    public abstract class GAgent : MonoBehaviour
    {
        public List<GAction> actions = new List<GAction>();
        public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
        public GAction currentAction;

        [SerializeField]
        protected string agentId;

        protected GPlanner planner;
        private Queue<GAction> actionQueue;
        private SubGoal currentGoal;

        private bool isRunning = true;

        protected abstract WorldStates GetWorldStates();
        private IWorldState worldState;

        [Inject]
        public void Construct(IWorldState worldState)
        {
            this.worldState = worldState;
        }

        public void SetRunning(bool isRunning)
        {
            this.isRunning = isRunning;
            planner = null;
            currentAction = null;
        }

        bool invoked = false;

        protected virtual void Start()
        {
            actions.ForEach(action => action.Init());
        }

        void CompleteAction()
        {
            currentAction.running = false;
            if (currentAction.PostPerform())
            {
                currentAction = null;
            }
            invoked = false;
        }

        protected virtual void LateUpdate()
        {
            if (!isRunning || !worldState.IsDayStarted())
            {
                return;
            }

            if (currentAction != null)
            {
                if (currentAction.running == false)
                {
                    if (currentAction.PrePerform())
                    {
                        currentAction.running = true;

                        if (currentAction.target != null)
                        {
                            var navMeshAgent = GetComponent<NavMeshAgent>();
                            navMeshAgent.SetDestination(currentAction.target.transform.position);
                        }
                    }
                    else
                    {
                        currentAction = null;
                        actionQueue = null;
                    }
                }
                else
                {
                    if (currentAction.IsDestinationReached())
                    {
                        if (!invoked)
                        {
                            Invoke("CompleteAction", currentAction.duration);
                            invoked = true;
                        }
                    }
                }

                return;
            }

            if (planner == null || actionQueue == null)
            {
                planner = new GPlanner();

                var sortedGoals = from entry in goals orderby entry.Value descending select entry;
           
                foreach(KeyValuePair<SubGoal, int> sg in sortedGoals)
                {
                    actionQueue = planner.plan(actions, sg.Key.sgoals, GetWorldStates());

                    if (actionQueue != null)
                    {
                        currentGoal = sg.Key;
                        break;
                    }
                }
            }

            if (actionQueue != null && actionQueue.Count == 0)
            {
                if (currentGoal.remove)
                {
                    goals.Remove(currentGoal);
                }
                planner = null;
            }

            if (actionQueue != null && actionQueue.Count > 0)
            {
                currentAction = actionQueue.Dequeue();
            }
        }
    }
}
