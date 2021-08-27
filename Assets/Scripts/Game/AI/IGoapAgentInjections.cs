using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
