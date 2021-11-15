
using System;
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
        event EventHandler Updated;
    }
}
