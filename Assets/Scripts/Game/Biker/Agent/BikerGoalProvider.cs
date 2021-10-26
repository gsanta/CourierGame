using AI;
using System;
using System.Collections.Generic;

namespace Bikers
{
    public class BikerGoalProvider : IGoalProvider
    {
        private readonly Biker biker;
        private SubGoal goal = new SubGoal("isPackageDropped", 1, true);

        public BikerGoalProvider(Biker biker)
        {
            this.biker = biker;
            biker.Agent.ActionCompleted += HandleCompleteAction;
            SetGoal();
        }

        public SubGoal GetGoal()
        {
            return goal;
        }

        public List<SubGoal> GetGoals()
        {
            return new List<SubGoal>() { goal };
        }

        private void HandleCompleteAction(object sender, EventArgs args)
        {
            SetGoal();
        }

        private void SetGoal()
        {
            biker.Agent.SetGoals(new List<SubGoal> { goal }, false);
        }
    }
}
