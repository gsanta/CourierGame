using System.Collections.Generic;

namespace AI
{
    public interface IPlanner<T> where T : IGameObject
    {
        Queue<GoapAction<T>> plan(List<GoapAction<T>> actions, Goal goal, AIStates states);
    }
}
