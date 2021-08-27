using System.Collections.Generic;

namespace AI
{
    public class SubGoal {
        public Dictionary<string, int> sgoals;
        public bool remove;
    
        public SubGoal(string sgoal, int i, bool remove)
        {
            sgoals = new Dictionary<string, int>();
            sgoals.Add(sgoal, i);
            this.remove = remove;
        }
    }
}
