using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Node<T> where T : IGameObject
    {
        public Node<T> parent;
        public float cost;
        public ISet<AIStateName> states;
        public GoapAction<T> action;

        public Node(Node<T> parent, float cost, ISet<AIStateName> states, GoapAction<T> action)
        {
            this.parent = parent;
            this.cost = cost;
            this.states = new HashSet<AIStateName>(states);
            this.action = action;
        }
    }

    public class GoapPlanner<T> : IPlanner<T> where T : IGameObject 
    {
        public Queue<GoapAction<T>> plan(List<GoapAction<T>> actions, Goal goal, AIStates states)
        {
            List<GoapAction<T>> usableActions = new List<GoapAction<T>>();
            foreach (GoapAction<T> a in actions)
            {
                if (a.IsAchievable())
                {
                    usableActions.Add(a);
                }
            }

            List<Node<T>> leaves = new List<Node<T>>();
            Node<T> start = new Node<T>(null, 0, states.GetStates(), null);

            bool success = BuildGraph(start, leaves, usableActions, goal);

            if (!success)
            {
                Debug.Log("No Plan found");
            }

            Node<T> cheapest = null;
            foreach (Node<T> leaf in leaves)
            {
                if (cheapest == null)
                {
                    cheapest = leaf;
                } else
                {
                    if (leaf.cost < cheapest.cost)
                    {
                        cheapest = leaf;
                    }
                }
            }

            List<GoapAction<T>> result = new List<GoapAction<T>>();
            Node<T> n = cheapest;
            while (n != null)
            {
                if (n.action != null)
                {
                    result.Insert(0, n.action);
                }
                n = n.parent;
            }

            Queue<GoapAction<T>> queue = new Queue<GoapAction<T>>();
            foreach (GoapAction<T> a in result)
            {
                queue.Enqueue(a);
            }

            return queue;
        }

        private bool BuildGraph(Node<T> parent, List<Node<T>> leaves, List<GoapAction<T>> usableActions, Goal goal)
        {
            bool foundPath = false;

            foreach (GoapAction<T> action in usableActions)
            {
                if (action.IsAchievableGiven(parent.states))
                {
                    ISet<AIStateName> currentStates = new HashSet<AIStateName>(parent.states);
                    foreach (AIStateName eff in action.GetAfterEffects())
                    {
                        if (!currentStates.Contains(eff))
                        {
                            currentStates.Add(eff);
                        }
                    }

                    Node<T> node = new Node<T>(parent, parent.cost + action.cost, currentStates, action);

                    if (GoalAchieved(goal, currentStates))
                    {
                        leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        List<GoapAction<T>> subset = ActionSubset(usableActions, action);
                        bool found = BuildGraph(node, leaves, subset, goal);

                        if (found)
                        {
                            foundPath = true;
                        }
                    }
                }
            }
            return foundPath;
        }

        private bool GoalAchieved(Goal goal, ISet<AIStateName> states)
        {
            foreach(AIStateName state in goal.states)
            {
                if (!states.Contains(state))
                {
                    return false;
                }
            }

            return true;
        }

        private List<GoapAction<T>> ActionSubset(List<GoapAction<T>> actions, GoapAction<T> removeMe)
        {
            List<GoapAction<T>> subset = new List<GoapAction<T>>();

            foreach (GoapAction<T> a in actions)
            {
                if (!a.Equals(removeMe))
                {
                    subset.Add(a);
                }
            }

            return subset;
        }
    }
}
