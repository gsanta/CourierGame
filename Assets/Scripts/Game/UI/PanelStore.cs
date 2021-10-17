using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PanelStore
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

        public List<GameObject> GetAllPanels()
        {
            return panelController.GetAllPanel();
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
