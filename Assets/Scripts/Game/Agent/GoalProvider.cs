
using System.Collections.Generic;

namespace Agents
{
    public class GoalTemplate
    {
        public readonly string targetName;
        public readonly float hideDuration;
        public readonly string goalName;

        public GoalTemplate(string targetName, float hideDuration, string goalName)
        {
            this.targetName = targetName;
            this.hideDuration = hideDuration;
            this.goalName = goalName;
        }
    }

    public class GoalProvider
    {
        private List<GoalTemplate> goals = new List<GoalTemplate>();

        public GoalProvider()
        {
            Init();
        }

        public List<GoalTemplate> GetGoals()
        {
            return goals;
        }

        private void Init()
        {
            goals.Add(new GoalTemplate("House 1", 3000, "goToHouse1"));
        }
    }
}
