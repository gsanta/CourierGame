using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Goal {
        public readonly bool remove;
        public readonly Transform? target;
        public readonly ISet<AIStateName> states;
        
        public int Priority { get; set; }

        public Goal(ISet<AIStateName> states, bool remove, Transform? target = null)
        {
            this.states = states;
            this.remove = remove;
            this.target = target;
            Priority = 1;
        }

        public Goal(AIStateName state, bool remove, Transform? target = null): this(new HashSet<AIStateName>(new AIStateName[] { state }), remove, target)
        {
       
        }
    }
}
