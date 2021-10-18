using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public interface IPanelController
    {
        T GetPanel<T>(Type type) where T : class;
        List<GameObject> GetAllPanels();
        void HidePanel(GameObject panel, float delay);
        void ShowPanel(GameObject panel, float delay);
    }
}
