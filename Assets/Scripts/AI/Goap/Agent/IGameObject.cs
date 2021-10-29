
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public interface IGameObject
    {
        GameObject GetGameObject();
        MonoBehaviour GetMonoBehaviour();
        NavMeshAgent GetNavMeshAgent();
        IGoalProvider GetGoalProvider();
    }
}
