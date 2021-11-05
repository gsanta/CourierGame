using System.Collections.Generic;

namespace AI
{
    public class AIStates
    {
        private ISet<AIStateName> states;

        public AIStates()
        {
            states = new HashSet<AIStateName>();
        }

        public bool HasState(AIStateName state)
        {
            return states.Contains(state);
        }

        public void AddState(AIStateName state)
        {
            states.Add(state);
        }

        public void AddStates(AIStateName[] newStates)
        {
            foreach(AIStateName state in newStates)
            {
                states.Add(state);
            }
        }

        public void RemoveState(AIStateName state)
        {
            if (states.Contains(state))
            {
                states.Remove(state);
            }
        }

        public ISet<AIStateName> GetStates()
        {
            return states;
        }
    }
}
