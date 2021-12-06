using GameObjects;
using UnityEngine.UI;

namespace UI
{
    public interface IBikerListItem
    {
        Button GetButton();
        bool IsActive { get; set; }

        void Destroy();
    }
}
