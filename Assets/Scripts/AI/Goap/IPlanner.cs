using System.Collections.Generic;

namespace AI
{
    public interface IPlanner<T>
    {
        Queue<GoapAction<T>> plan(List<GoapAction<T>> actions, Dictionary<string, int> goal, WorldStates states);
    }
}
