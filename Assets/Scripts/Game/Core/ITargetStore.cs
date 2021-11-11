
using System.Collections.Generic;

namespace Scenes
{
    public interface ITargetStore<T>
    {
        void SetTargets(List<T> targets);
    }
}
