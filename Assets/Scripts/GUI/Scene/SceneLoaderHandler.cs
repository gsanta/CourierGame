using Scenes;
using UI;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class SceneLoaderHandler : MonoBehaviour
    {
        [SerializeField]
        private string[] initialScenes;
        [SerializeField]
        private string[] mapScenes;
        [SerializeField]
        private string[] initialPanels;
        private SceneLoader sceneLoader;
        private SceneChangeHandler sceneChangeHandler;
        private CanvasInitializer canvasInitializer;

        [Inject]
        public void Construct(SceneLoader sceneLoader, SceneChangeHandler sceneChangeHandler, CanvasInitializer canvasInitializer)
        {
            this.sceneChangeHandler = sceneChangeHandler;
            this.sceneLoader = sceneLoader;
            this.canvasInitializer = canvasInitializer;
        }

        private void Awake()
        {
            sceneLoader.SetScenes(initialScenes, mapScenes);
            canvasInitializer.SetInitialPanels(initialPanels);
        }

        private void Start()
        {
            //sceneChangeHandler.InitCurrentScene();
        }
    }
}
