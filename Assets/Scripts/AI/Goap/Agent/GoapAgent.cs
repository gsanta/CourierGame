using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class GoapAgent<T>
    {
        public List<GoapAction<T>> actions = new List<GoapAction<T>>();
        public GoapAction<T> currentAction;

        public WorldStates worldStates = new WorldStates();

        protected GoapPlanner<T> planner;
        private Queue<GoapAction<T>> actionQueue;
        private SubGoal currentGoal;

        private string agentId;
        private T parent;
        private MonoBehaviour monoBehaviour;
        private readonly IGoalProvider goalProvider;
        private NavMeshAgent navMeshAgent;
        private bool isActive = false;

        public GoapAgent(string agentId, T parent, NavMeshAgent navMeshAgent, MonoBehaviour monoBehaviour, IGoalProvider goalProvider)
        {
            this.agentId = agentId;
            this.parent = parent;
            this.monoBehaviour = monoBehaviour;
            this.goalProvider = goalProvider;
            this.navMeshAgent = navMeshAgent;
        }

        public void SetActions(List<GoapAction<T>> actions)
        {
            this.actions = actions;
            actions.ForEach(action => action.Init());
        }

        public void SetGoals()
        {
            this.actions = actions;
            actions.ForEach(action => action.Init());
        }

        public T Parent { get => parent; }

        public NavMeshAgent NavMeshAgent { get => navMeshAgent; }

        public string AgentId { get => agentId; }

        bool invoked = false;

        public bool Active
        {
            get => isActive;
            set {
                if (isActive != value)
                {
                    isActive = value;
                    if (!isActive)
                    {
                        AbortAction();
                    }
                }
            }

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
                HandleAction();
            } else
            {
                FindAction();
            }
        }

        private void HandleAction()
        {
            if (currentAction.running == false)
            {
                if (currentAction.PrePerform())
                {
                    currentAction.running = true;
                }
                else
                {
                    currentAction = null;
                    actionQueue = null;
                }
            }
            else
            {
                currentAction.Update();

                if (currentAction.Finished)
                {
                    if (!invoked)
                    {
                        monoBehaviour.Invoke("CompleteAction", currentAction.duration);
                        invoked = true;
                    }
                }
            }
        }

        private void FindAction()
        {
            if (planner == null || actionQueue == null)
            {
                planner = new GoapPlanner<T>();

                var subGoal = goalProvider.GetGoal();

                actionQueue = planner.plan(actions, subGoal.sgoals, worldStates);

                if (actionQueue != null)
                {
                    currentGoal = subGoal;
                }
            }

            if (actionQueue != null && actionQueue.Count == 0)
            {
                actionQueue = null;
                planner = null;
            }

            if (actionQueue != null && actionQueue.Count > 0)
            {
                currentAction = actionQueue.Dequeue().Clone(this);
            }

        }
    }
}
