using System;
using UnityEngine;

namespace UI
{
    public interface IPanelController
    {
        T GetPanel<T>(Type type) where T : class;
        void HidePanel(GameObject panel, float delay);
        void ShowPanel(GameObject panel, float delay);
    }
}
