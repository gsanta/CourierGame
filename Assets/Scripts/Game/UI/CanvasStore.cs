using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CanvasStore
    {
        private IPanelController panelController;
        private Dictionary<string, GameObject> panelMap = new Dictionary<string, GameObject>();
        private GameObject canvasContainer;

        public void SetCanvas(GameObject canvasContainer)
        {
            this.canvasContainer = canvasContainer;
            panelController = canvasContainer.GetComponent<IPanelController>();

            foreach (Transform child in canvasContainer.transform)
            {
                panelMap.Add(child.gameObject.name, child.gameObject);
            }
        }

        public T GetPanel<T>(Type type) where T : class
        {
            return canvasContainer.GetComponentsInChildren(type, true)[0] as T;
        }

        public void HidePanel(Type type)
        {
            GetPanel<MonoBehaviour>(type).gameObject.SetActive(false);
        }

        public void ShowPanel(Type type)
        {
            GetPanel<MonoBehaviour>(type).gameObject.SetActive(true);
        }

        public List<GameObject> GetAllPanels()
        {
            return panelController.GetAllPanels();
        }

        public GameObject GetPanelByName(string name)
        {
            return panelMap[name];
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
