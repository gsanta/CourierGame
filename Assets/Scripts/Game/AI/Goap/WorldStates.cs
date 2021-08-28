using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AI
{

    [System.Serializable]
    public class WorldState
    {
        public readonly string key;
        public readonly int value;

        public WorldState(string key, int value)
        {
            this.key = key;
            this.value = value;
        }
    }

    public class WorldStates
    {
        private Dictionary<string, int> states;

        public WorldStates()
        {
            states = new Dictionary<string, int>();
        }

        public bool HasState(string key)
        {
            return states.ContainsKey(key);
        }

        public void AddState(string key, int value)
        {
            states.Add(key, value);
        }

        public void ModifyState(string key, int value)
        {
            states[key] += value;
            if (states[key] <= 0)
            {
                RemoveState(key);
            }
        }

        public void RemoveState(string key)
        {
            if (states.ContainsKey(key))
            {
                states.Remove(key);
            }
        }

        public void SetState(string key, int value)
        {
            if (states.ContainsKey(key))
            {
                states[key] = value;
            } else
            {
                states.Add(key, value);
            }
        }

        public Dictionary<string, int> ToDictionary()
        {
            return states;
        }
    }
}
