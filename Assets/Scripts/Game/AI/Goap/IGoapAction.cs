using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    interface IGoapAction<out T> where T : GAgent<T>
    {
        void Init();
        bool IsAchievable();
        bool IsAchievableGiven(Dictionary<string, int> conditions);

        WorldState[] GetPreConditions();
        WorldState[] GetAfterEffects();
        bool PrePerform();
        bool PostPerform();
        bool IsDestinationReached();

    }
}
