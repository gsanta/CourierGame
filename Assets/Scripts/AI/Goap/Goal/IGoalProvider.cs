using System.Collections.Generic;

namespace AI

{
    public interface IGoalProvider
    {
        List<SubGoal> GetGoals();
        SubGoal GetGoal();
    }
}
