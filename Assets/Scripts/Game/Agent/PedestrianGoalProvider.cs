﻿
using AI;
using Pedestrians;
using System;
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

    public class PedestrianGoalProvider : IGoalProvider
    {
        private readonly PedestrianTargetStore pedestrianTargetStore;
        private List<SubGoal> goals = new List<SubGoal>();
        private Pedestrian pedestrian;

        public PedestrianGoalProvider(Pedestrian pedestrian, PedestrianTargetStore pedestrianTargetStore)
        {
            this.pedestrian = pedestrian;
            pedestrian.agent.ActionCompleted += HandleCompleteAction;
            this.pedestrianTargetStore = pedestrianTargetStore;
            Init();
            SetGoal();
        }

        public List<SubGoal> GetGoals()
        {
            return goals;
        }

        public SubGoal GetGoal()
        {
            var goalIndex = UnityEngine.Random.Range(0, goals.Count - 1);
            return goals[goalIndex];
        }

        private void Init()
        {
            pedestrianTargetStore.GetTargets().ForEach(target =>
            {
                goals.Add(new SubGoal("goto" + target.name, target.priority, false));
            });
        }

        private void HandleCompleteAction(object sender, EventArgs args)
        {
            SetGoal();
        }

        private void SetGoal()
        {
            pedestrian.agent.SetGoals(new List<SubGoal> { GetGoal() }, false);
        }
    }
}