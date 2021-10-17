
using Core;
using UI;
using UnityEngine;

namespace GUI
{
    public class SceneInitializer : MonoBehaviour, ISceneInitializer
    {
        private readonly PanelStore panelStore;

        public SceneInitializer(PanelStore panelStore)
        {
            this.panelStore = panelStore;
        }

        public void InitializeScene()
        {
            panelStore.GetAllPanels().ForEach(panel => panel.SetActive(false));
            panelStore.GetPanel<StartDayPanel>(typeof(StartDayPanel)).gameObject.SetActive(true);
        }
    }
}
