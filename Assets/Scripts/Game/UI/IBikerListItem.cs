using Bikers;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public interface IBikerListItem
    {
        Biker GetBiker();

        Button GetButton();

        void Destroy();
    }
}
