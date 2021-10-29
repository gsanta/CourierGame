
using System.Collections.Generic;

namespace Core
{
    public interface ITargetStore<T>
    {
        void SetTargets(List<T> targets);
    }
}
