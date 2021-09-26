﻿using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public abstract class GoapAction<T>
    {
        public string actionName = "Action";
        public float cost = 1.0f;
        public Vector3 target;
        public string targetTag;
        public float duration = 1;
        public Dictionary<string, int> preConditionsDict;
        public Dictionary<string, int> effectsDict;
        public WorldStates agentBeliefs;

        public bool running = false;
        protected bool finished = false;

        protected IGoapAgentProvider<T> agent;

        public GoapAction(IGoapAgentProvider<T> agent)
        {
            this.agent = agent;
            preConditionsDict = new Dictionary<string, int>();
            effectsDict = new Dictionary<string, int>();
        }

        public bool Finished { get => finished; }

        public GoapAgent<T> GoapAgent { get => agent.GetGoapAgent(); }

        public void Init()
        {
            foreach (WorldState w in GetPreConditions())
            {
                preConditionsDict.Add(w.key, w.value);
            }

            foreach (WorldState w in GetAfterEffects())
            {
                effectsDict.Add(w.key, w.value);
            }
        }

        public bool IsAchievable()
        {
            return true;
        }

        public bool IsAchievableGiven(Dictionary<string, int> conditions)
        {
            foreach (KeyValuePair<string, int> p in preConditionsDict)
            {
                if (!conditions.ContainsKey(p.Key))
                {
                    return false;
                }
            }
            return true;
        }

        protected abstract WorldState[] GetPreConditions();
        protected abstract WorldState[] GetAfterEffects();
        public abstract bool PrePerform();
        public abstract bool PostPerform();
        public abstract bool PostAbort();
        public virtual void Update() { }
    }
}