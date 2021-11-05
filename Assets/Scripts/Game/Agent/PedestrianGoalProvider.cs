
using AI;
using Pedestrians;
using System;
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

    public static class PedestrianGoalNames
    {
        public static string[] names = new string[] {
            "goToHouse1",
            "goToHouse2"
        }; 
    }

    public class PedestrianGoalProvider : IGoalProvider
    {
        private readonly WalkTargetStore walkTargetStore;
        private List<Goal> goals = new List<Goal>();
        private Pedestrian pedestrian;

        public PedestrianGoalProvider(Pedestrian pedestrian, WalkTargetStore walkTargetStore)
        {
            this.pedestrian = pedestrian;
            pedestrian.Agent.ActionCompleted += HandleCompleteAction;
            this.walkTargetStore = walkTargetStore;
            Init();
            SetGoal();
        }

        public List<Goal> CreateGoal()
        {
            var goalIndex = UnityEngine.Random.Range(0, goals.Count - 1);
            return new List<Goal> { goals[goalIndex] };
        }

        public List<Goal> GetGoals()
        {
            return goals;
        }

        public Goal GetGoal()
        {
            var goalIndex = UnityEngine.Random.Range(0, goals.Count - 1);
            return goals[goalIndex];
        }

        private void Init()
        {
            walkTargetStore.GetTargets().ForEach(target =>
            {
                goals.Add(new Goal(AIStateName.DESTINATION_REACHED, false, target.transform.position));
            });
        }

        private void HandleCompleteAction(object sender, EventArgs args)
        {
            //SetGoal();
        }

        private void SetGoal()
        {
            pedestrian.Agent.SetGoals(new List<Goal> { GetGoal() }, false);
        }
    }
}
