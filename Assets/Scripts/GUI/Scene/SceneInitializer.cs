
using Core;
using Scenes;
using UI;
using UnityEngine;

namespace GUI
{
    public class SceneInitializer : MonoBehaviour, ISceneInitializer
    {
        private readonly PanelStore panelStore;
        private readonly SceneChangeHandler sceneChangeHandler;

        public SceneInitializer(PanelStore panelStore, SceneChangeHandler sceneChangeHandler)
        {
            this.panelStore = panelStore;
            this.sceneChangeHandler = sceneChangeHandler;
        }

        private void Awake()
        {
            sceneChangeHandler.
        }

        public void InitializeScene()
        {
            panelStore.GetAllPanels().ForEach(panel => panel.SetActive(false));
            panelStore.GetPanel<StartDayPanel>(typeof(StartDayPanel)).gameObject.SetActive(true);
        }
    }
}
