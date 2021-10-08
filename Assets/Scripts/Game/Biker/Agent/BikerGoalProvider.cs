using AI;

namespace Bikers
{
    public class BikerGoalProvider : IGoalProvider
    {

        private SubGoal goal = new SubGoal("isPackageDropped", 1, true);

        public SubGoal GetGoal()
        {
            return goal;
        }
    }
}
