using Agents;
using AI;
using System.Collections.Generic;
using System.Linq;

namespace Pedestrians
{
    public class SimplePlanner<T> : IPlanner<T> where T : IGameObject
    {
        //public Queue<GoapAction<Pedestrian>> plan(List<GoapAction<Pedestrian>> actions, Dictionary<string, int> goal, AIStates states)
        //{
        //    IEnumerator<KeyValuePair<string, int>> enumerator = goal.GetEnumerator();
        //    enumerator.MoveNext();

        //    KeyValuePair<string, int> firstGoal = enumerator.Current;
        //    var action = actions.Find(action => action.afterEffect.key == firstGoal.Key);
            
        //    if (action != null)
        //    {
        //        return new Queue<GoapAction<Pedestrian>>(new List<GoapAction<Pedestrian>> { action });
        //    }
        //    return null;
        //}

        public Queue<GoapAction<T>> plan(List<GoapAction<T>> actions, Goal goal, AIStates states)
        {
            AIStateName state = goal.states.ToList()[0];
            var action = actions.Find(action => action.GetAfterEffectsSet().Contains(state));

            if (action != null)
            {
                return new Queue<GoapAction<T>>(new List<GoapAction<T>> { action });
            }
            return null;
        }
    }
}
