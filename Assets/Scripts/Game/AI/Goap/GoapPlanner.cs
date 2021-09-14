using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Node<T>
    {
        public Node<T> parent;
        public float cost;
        public Dictionary<string, int> state;
        public GoapAction<T> action;

        public Node(Node<T> parent, float cost, Dictionary<string, int> allStates, GoapAction<T> action)
        {
            this.parent = parent;
            this.cost = cost;
            this.state = new Dictionary<string, int>(allStates);
            this.action = action;
        }
    }

    public class GoapPlanner<T>
    {
        public Queue<GoapAction<T>> plan(List<GoapAction<T>> actions, Dictionary<string, int> goal, WorldStates states)
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

            Debug.Log("The Plan is: ");
            foreach (GoapAction<T> a in queue)
            {
                Debug.Log("Q: " + a.actionName);
            }

            return queue;
        }

        private bool BuildGraph(Node<T> parent, List<Node<T>> leaves, List<GoapAction<T>> usableActions, Dictionary<string, int> goal)
        {
            bool foundPath = false;

            foreach (GoapAction<T> action in usableActions)
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
