using System.Collections.Generic;

namespace AI

{
    public interface IGoalProvider
    {
        List<SubGoal> CreateGoal();
        List<SubGoal> GetGoals();
        SubGoal GetGoal();
    }
}
