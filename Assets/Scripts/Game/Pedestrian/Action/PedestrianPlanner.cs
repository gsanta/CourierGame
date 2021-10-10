using Agents;
using AI;
using System.Collections.Generic;

namespace Pedestrians
{
    public class PedestrianPlanner : IPlanner<Pedestrian>
    {
        private readonly ActionProvider actionProvider;

        public PedestrianPlanner(ActionProvider actionProvider)
        {
            this.actionProvider = actionProvider;
        }

        public Queue<GoapAction<Pedestrian>> plan(List<GoapAction<Pedestrian>> actions, Dictionary<string, int> goal, WorldStates states)
        {
            IEnumerator<KeyValuePair<string, int>> enumerator = goal.GetEnumerator();
            enumerator.MoveNext();

            KeyValuePair<string, int> firstGoal = enumerator.Current;
            var action = actionProvider.GetByAfterEffect(firstGoal.Key);
            return new Queue<GoapAction<Pedestrian>>(new List<GoapAction<Pedestrian>> { action });
        }
    }
}
