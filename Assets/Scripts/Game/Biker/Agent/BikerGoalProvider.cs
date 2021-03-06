using AI;
using System;
using System.Collections.Generic;

namespace GameObjects
{
    public class BikerGoalProvider : IGoalProvider
    {
        private readonly GameCharacter biker;
        private Goal goal = new Goal(AIStateName.PACKAGE_IS_DROPPED, true);

        public BikerGoalProvider(GameCharacter biker)
        {
            this.biker = biker;
            biker.Agent.ActionCompleted += HandleCompleteAction;
            SetGoal();
        }

        public List<Goal> CreateGoal()
        {
            throw new NotImplementedException();
        }

        public Goal GetGoal()
        {
            return goal;
        }

        public List<Goal> GetGoals()
        {
            return new List<Goal>() { goal };
        }

        private void HandleCompleteAction(object sender, EventArgs args)
        {
            SetGoal();
        }

        private void SetGoal()
        {
            biker.Agent.SetGoals(new List<Goal> { goal }, false);
        }
    }
}
