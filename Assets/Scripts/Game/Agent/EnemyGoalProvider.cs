using AI;
using Enemies;
using Pedestrians;
using System.Collections.Generic;

namespace Agents
{
    public class EnemyGoalProvider : IGoalProvider
    {
        private readonly WalkTargetStore walkTargetStore;
        private List<SubGoal> goals = new List<SubGoal>();
        private Enemy enemy;

        public EnemyGoalProvider(Enemy enemy, WalkTargetStore walkTargetStore)
        {
            this.enemy = enemy;
            this.walkTargetStore = walkTargetStore;
            Init();
            SetGoal();
        }

        public List<SubGoal> CreateGoal()
        {
            var goalIndex = UnityEngine.Random.Range(0, goals.Count - 1);
            return new List<SubGoal> { goals[goalIndex] };
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
            walkTargetStore.GetTargets().ForEach(target =>
            {
                goals.Add(new SubGoal("goto" + target.name, target.priority, false));
            });
        }

        private void SetGoal()
        {
            enemy.Agent.SetGoals(new List<SubGoal> { GetGoal() }, false);
        }
    }
}
