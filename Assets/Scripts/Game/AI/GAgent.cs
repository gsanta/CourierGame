﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using AI;

//namespace AI
//{

    public class GAgent<T> : MonoBehaviour
    {
        public List<GAction<T>> actions = new List<GAction<T>>();
        public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
        public GAction<T> currentAction;

        [SerializeField]
        public string agentId;

        protected GPlanner<T> planner;
        private Queue<GAction<T>> actionQueue;
        private SubGoal currentGoal;

        private bool isRunning = true;

        public T Parent { get; set; }

        public void SetRunning(bool isRunning)
        {
            this.isRunning = isRunning;
            planner = null;
            if (currentAction != null) {
                currentAction.running = false;
            }
            currentAction = null;
        }

        bool invoked = false;

        protected virtual void Start()
        {
            actions.ForEach(action => action.Init());
        }

        void CompleteAction()
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

        protected virtual void LateUpdate()
        {
            if (!isRunning)
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
                planner = new GPlanner<T>();

                var sortedGoals = from entry in goals orderby entry.Value descending select entry;
           
                foreach(KeyValuePair<SubGoal, int> sg in sortedGoals)
                {
                    actionQueue = planner.plan(actions, sg.Key.sgoals, null);

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
//}
