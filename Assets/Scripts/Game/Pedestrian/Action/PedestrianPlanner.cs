using Agents;
using AI;
using System.Collections.Generic;

namespace Pedestrians
{
    public class PedestrianPlanner : IPlanner<Pedestrian>
    {
        public Queue<GoapAction<Pedestrian>> plan(List<GoapAction<Pedestrian>> actions, Dictionary<string, int> goal, AIStates states)
        {
            IEnumerator<KeyValuePair<string, int>> enumerator = goal.GetEnumerator();
            enumerator.MoveNext();

            KeyValuePair<string, int> firstGoal = enumerator.Current;
            var action = actions.Find(action => action.afterEffect.key == firstGoal.Key);
            
            if (action != null)
            {
                return new Queue<GoapAction<Pedestrian>>(new List<GoapAction<Pedestrian>> { action });
            }
            return null;
        }
    }
}
