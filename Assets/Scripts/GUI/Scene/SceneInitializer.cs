
using Core;
using Scenes;
using UI;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class SceneInitializer : MonoBehaviour, ISceneInitializer
    {
        private PanelStore panelStore;
        private SceneChangeHandler sceneChangeHandler;

        [Inject]
        public void Construct(PanelStore panelStore, SceneChangeHandler sceneChangeHandler)
        {
            this.panelStore = panelStore;
            this.sceneChangeHandler = sceneChangeHandler;
        }

        private void Awake()
        {
            sceneChangeHandler.SetSceneInitializer(this);
        }

        public void InitializeScene()
        {
            panelStore.GetAllPanels().ForEach(panel => panel.SetActive(false));
            panelStore.GetPanel<StartDayPanel>(typeof(StartDayPanel)).gameObject.SetActive(true);
        }
    }
}
