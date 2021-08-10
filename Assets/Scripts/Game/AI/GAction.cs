using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public abstract class GAction : MonoBehaviour
    {
        public string actionName = "Action";
        public float cost = 1.0f;
        public GameObject target;
        public GameObject targetTag;
        public float duration;
        public WorldState[] preConditions;
        public WorldState[] afterEffects;
        public NavMeshAgent agent;
        public Dictionary<string, int> preConditionsDict;
        public Dictionary<string, int> effectsDict;

        public WorldStates agentBeliefs;

        public bool running = false;

        public GAction()
        {
            preConditionsDict = new Dictionary<string, int>();
            effectsDict = new Dictionary<string, int>();
        }

        public void Awake()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();

            if (preConditions != null)
            {
                foreach (WorldState w in preConditions)
                {
                    preConditionsDict.Add(w.key, w.value);
                }
            }

            if (afterEffects != null)
            {
                foreach (WorldState w in afterEffects)
                {
                    effectsDict.Add(w.key, w.value);
                }
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

        public abstract bool PrePerform();
        public abstract bool PostPerform();
    }
}
