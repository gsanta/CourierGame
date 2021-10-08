
using AI;
using Pedestrians;
using System.Collections.Generic;
using UnityEngine;

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

    public static class PedestrianGoalNames
    {
        public static string[] names = new string[] {
            "goToHouse1",
            "goToHouse2"
        }; 
    }

    public class GoalProvider : IGoalProvider
    {
        private readonly PedestrianTargetStore pedestrianTargetStore;
        private List<SubGoal> goals = new List<SubGoal>();

        public GoalProvider(PedestrianTargetStore pedestrianTargetStore)
        {
            this.pedestrianTargetStore = pedestrianTargetStore;
            Init();
        }

        public List<SubGoal> GetGoals()
        {
            return goals;
        }

        public SubGoal GetGoal()
        {
            var goalIndex = Random.Range(0, goals.Count - 1);
            return goals[goalIndex];
        }

        private void Init()
        {
            pedestrianTargetStore.GetTargets().ForEach(target =>
            {
                goals.Add(new SubGoal("goto" + target.name, target.priority, false));
            });
        }
    }
}
