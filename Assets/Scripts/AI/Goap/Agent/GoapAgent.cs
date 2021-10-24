using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class GoapAgent<T> where T : IGameObject
    {
        public List<GoapAction<T>> actions = new List<GoapAction<T>>();
        public WorldStates worldStates = new WorldStates();
        private Queue<GoapAction<T>> actionQueue;
        private List<SubGoal> goals = new List<SubGoal>();

        private string agentId;
        private T parent;
        private GameObject gameObject;
        private readonly IPlanner<T> planner;
        private NavMeshAgent navMeshAgent;
        private bool isActive = false;

        public GoapAction<T> currentAction;
        public GoapAction<T> prevAction;

        public GoapAgent(string agentId, T parent, IPlanner<T> planner)
        {
            this.agentId = agentId;
            this.parent = parent;
            this.planner = planner;
            gameObject = parent.GetGameObject();
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        }

        public void SetGoals(List<SubGoal> goals, bool abortCurrentAction)
        {
            this.goals = goals;
            if (abortCurrentAction)
            {
                AbortAction();
            }
        }

        public void SetActions(List<GoapAction<T>> actions)
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

        public event EventHandler ActionCompleted;

        public void CompleteAction()
        {
            if (currentAction != null)
            {
                currentAction.running = false;
                if (currentAction.PostPerform())
                {
                    prevAction = currentAction;
                    currentAction = null;
                }
                invoked = false;
                ActionCompleted?.Invoke(this, EventArgs.Empty);
            }
        }

        public void AbortAction()
        {
            if (currentAction != null)
            {
                currentAction.running = false;
                currentAction.PostAbort();
                prevAction = currentAction;
                currentAction = null;
            }
            actionQueue = null;
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
                        parent.GetMonoBehaviour().Invoke("CompleteAction", currentAction.duration);
                        invoked = true;
                    }
                }
            }
        }

        private void FindAction()
        {
            if (actionQueue == null && goals.Count > 0)
            {

                var subGoal = goals[0];
                goals.RemoveAt(0);

                var startTime = DateTime.Now;
                actionQueue = planner.plan(actions, subGoal.sgoals, worldStates);
                Debug.Log((DateTime.Now - startTime).Milliseconds);
            }

            if (actionQueue != null && actionQueue.Count == 0)
            {
                actionQueue = null;
            }

            if (actionQueue != null && actionQueue.Count > 0)
            {
                currentAction = actionQueue.Dequeue().Clone(this);
            }

        }
    }
}
