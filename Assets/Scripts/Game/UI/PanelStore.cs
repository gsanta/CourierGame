using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PanelStore
    {
        private IPanelController panelController;
        private IToolbarController widgetController;

        public void AddPanelController(IPanelController panelController)
        {
            this.panelController = panelController;
        }

        public void AddWidgetController(IToolbarController widgetController)
        {
            this.widgetController = widgetController;
        }

        public T GetPanel<T>(Type type) where T : class
        {
            return panelController.GetPanel<T>(type);
        }

        public List<GameObject> GetAllPanels()
        {
            return panelController.GetAllPanels();
        }

        public T GetWidget<T>(Type type) where T : class
        {
            return widgetController.GetWidget<T>(type);
        }

        public List<GameObject> GetAllWidgets()
        {
            return widgetController.GetAllWidgets();
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
