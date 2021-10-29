
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface ITargetStore
    {
        void SetTargets(List<GameObject> targets);
    }
}
