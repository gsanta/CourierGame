using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

namespace Service.AI
{
    public class GoapAgent<T>
    {
        public List<GoapAction<T>> actions = new List<GoapAction<T>>();
        public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
        public GoapAction<T> currentAction;

        protected GoapPlanner<T> planner;
        private Queue<GoapAction<T>> actionQueue;
        private SubGoal currentGoal;
        private IGoapAgentInjections<T> goapAgentInjections;

        private string agentId;

        public GoapAgent(string agentId, IGoapAgentInjections<T> goapAgentInjections, List<GoapAction<T>> actions, Dictionary<SubGoal, int> goals)
        {
            this.agentId = agentId;
            this.goapAgentInjections = goapAgentInjections;
            this.actions = actions;
            this.goals = goals;

            Start();
        }

        public T Parent { get => goapAgentInjections.GetCharachter(); }

        public NavMeshAgent NavMeshAgent { get => goapAgentInjections.GetNavMeshAgent(); }

        public string AgentId { get => agentId; }

        bool invoked = false;

        public void Start()
        {
            actions.ForEach(action => action.Init());
        }

        public void CompleteAction()
        {
            if (currentAction != null)
            {
                currentAction.running = false;
                if (currentAction.PostPerform())
                {
                    currentAction = null;
                }
                invoked = false;
            }
        }

        public void AbortAction()
        {
            if (currentAction != null)
            {
                currentAction.running = false;
                currentAction.PostAbort();
                currentAction = null;
            }
            planner = null;
        }

        public void Update()
        {
            if (currentAction != null)
            {
                if (currentAction.running == false)
                {
                    if (currentAction.PrePerform())
                    {
                        currentAction.running = true;

                        if (currentAction.target != null)
                        {
                            NavMeshAgent navMeshAgent = goapAgentInjections.GetNavMeshAgent();
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
                    {
                        if (!invoked)
                        {
                            goapAgentInjections.Invoke("CompleteAction", currentAction.duration);
                            invoked = true;
                        }
                    }
                }

                return;
            }

            if (planner == null || actionQueue == null)
            {
                planner = new GoapPlanner<T>();

                var sortedGoals = from entry in goals orderby entry.Value descending select entry;

                foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
                {
                    actionQueue = planner.plan(actions, sg.Key.sgoals, goapAgentInjections.GetWorldStates());

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
