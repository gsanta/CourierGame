using System.Collections.Generic;

namespace AI

{
    public interface IGoalProvider
    {
        List<Goal> CreateGoal();
        List<Goal> GetGoals();
        Goal GetGoal();
    }
}
