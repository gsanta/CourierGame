using System.Collections.Generic;

namespace AI
{

    [System.Serializable]
    public class AIState
    {
        public readonly string key;
        public readonly int value;

        public AIState(string key, int value)
        {
            this.key = key;
            this.value = value;
        }
    }

    public class AIStates
    {
        private Dictionary<string, int> states;

        public AIStates()
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
