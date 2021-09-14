using UnityEngine.AI;

namespace AI
{
    public interface IGoapAgentInjections<T>
    {
        NavMeshAgent GetNavMeshAgent();

        T GetCharachter();
        void Invoke(string methodName, float time);
        WorldStates GetWorldStates();
    }
}
