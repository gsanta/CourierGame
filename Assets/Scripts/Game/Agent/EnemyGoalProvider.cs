using AI;
using Enemies;
using Pedestrians;
using System.Collections.Generic;

namespace Agents
{
    public class EnemyGoalProvider : IGoalProvider
    {
        private readonly WalkTargetStore walkTargetStore;
        private List<Goal> goals = new List<Goal>();
        private Enemy enemy;

        public EnemyGoalProvider(Enemy enemy, WalkTargetStore walkTargetStore)
        {
            this.enemy = enemy;
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
                goals.Add(new Goal(AIStateName.DESTINATION_REACHED, false, target.transform));
            });
        }

        private void SetGoal()
        {
            enemy.Agent.SetGoals(new List<Goal> { GetGoal() }, false);
        }
    }
}
