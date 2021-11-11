
using Scenes;
using Scenes;
using UI;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class SceneInitializer : MonoBehaviour, ISceneInitializer
    {
        private PanelStore panelStore;
        private SceneChangeHandler sceneChangeHandler;
        private ISceneSetup sceneSetup;

        [Inject]
        public void Construct(PanelStore panelStore, SceneChangeHandler sceneChangeHandler)
        {
            this.panelStore = panelStore;
            this.sceneChangeHandler = sceneChangeHandler;
        }

        public void SetSceneSetup(ISceneSetup sceneSetup)
        {
            this.sceneSetup = sceneSetup;
        }

        private void Awake()
        {
            sceneChangeHandler.SetSceneInitializer(this);
        }

        private void Start()
        {
            sceneSetup.SetupScene();
        }

        public void InitializeScene()
        {
            panelStore.GetAllPanels().ForEach(panel => panel.SetActive(false));
            panelStore.GetPanel<StartDayPanel>(typeof(StartDayPanel)).gameObject.SetActive(true);
            panelStore.GetPanel<TopPanelController>(typeof(TopPanelController)).gameObject.SetActive(true);
        }
    }
}
