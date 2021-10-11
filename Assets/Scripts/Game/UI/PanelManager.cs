using System;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class PanelManager
    {
        private IPanelController panelController;

        public void AddController(IPanelController panelController)
        {
            this.panelController = panelController;
        }

        public T GetPanel<T>(Type type) where T : class
        {
            return panelController.GetPanel<T>(type);
        }

        public void HidePanel(GameObject panel, float delay)
        {
            if (panelController != null)
            {
                panelController.HidePanel(panel, delay);
            }
        }

        public void ShowPanel(GameObject panel, float delay)
        {
            if (panelController != null)
            {
                panelController.ShowPanel(panel, delay);
            }
        }
    }
}
