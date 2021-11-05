using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Goal {
        public readonly bool remove;
        public readonly Vector3? target;
        public readonly ISet<AIStateName> states;
        
        public int Priority { get; set; }

        public Goal(ISet<AIStateName> states, bool remove, Vector3? target = null)
        {
            this.states = states;
            this.remove = remove;
            this.target = target;
            Priority = 1;
        }

        public Goal(AIStateName state, bool remove, Vector3? target = null): this(new HashSet<AIStateName>(new AIStateName[] { state }), remove, target)
        {
       
        }
    }
}
