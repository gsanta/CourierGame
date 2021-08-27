using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI
{
    public class Node<T>
    {
        public Node<T> parent;
        public float cost;
        public Dictionary<string, int> state;
        public GAction<T> action;

        public Node(Node<T> parent, float cost, Dictionary<string, int> allStates, GAction<T> action)
        {
            this.parent = parent;
            this.cost = cost;
            this.state = new Dictionary<string, int>(allStates);
            this.action = action;
        }
    }

    public class GPlanner<T>
    {
        public Queue<GAction<T>> plan(List<GAction<T>> actions, Dictionary<string, int> goal, WorldStates states)
        {
            List<GAction<T>> usableActions = new List<GAction<T>>();
            foreach (GAction<T> a in actions)
            {
                if (a.IsAchievable())
                {
                    usableActions.Add(a);
                }
            }

            List<Node<T>> leaves = new List<Node<T>>();
            Node<T> start = new Node<T>(null, 0, states.ToDictionary(), null);

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

            List<GAction<T>> result = new List<GAction<T>>();
            Node<T> n = cheapest;
            while (n != null)
            {
                if (n.action != null)
                {
                    result.Insert(0, n.action);
                }
                n = n.parent;
            }

            Queue<GAction<T>> queue = new Queue<GAction<T>>();
            foreach (GAction<T> a in result)
            {
                queue.Enqueue(a);
            }

            Debug.Log("The Plan is: ");
            foreach (GAction<T> a in queue)
            {
                Debug.Log("Q: " + a.actionName);
            }

            return queue;
        }

        private bool BuildGraph(Node<T> parent, List<Node<T>> leaves, List<GAction<T>> usableActions, Dictionary<string, int> goal)
        {
            bool foundPath = false;

            foreach (GAction<T> action in usableActions)
            {
                if (action.IsAchievableGiven(parent.state))
                {
                    Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                    foreach (KeyValuePair<string, int> eff in action.effectsDict)
                    {
                        if (!currentState.ContainsKey(eff.Key))
                        {
                            currentState.Add(eff.Key, eff.Value);
                        }
                    }

                    Node<T> node = new Node<T>(parent, parent.cost + action.cost, currentState, action);

                    if (GoalAchieved(goal, currentState))
                    {
                        leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        List<GAction<T>> subset = ActionSubset(usableActions, action);
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

        private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
        {
            foreach(KeyValuePair<string, int> g in goal)
            {
                if (!state.ContainsKey(g.Key))
                {
                    return false;
                }
            }

            return true;
        }

        private List<GAction<T>> ActionSubset(List<GAction<T>> actions, GAction<T> removeMe)
        {
            List<GAction<T>> subset = new List<GAction<T>>();

            foreach (GAction<T> a in actions)
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
