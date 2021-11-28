using Scenes;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Worlds;
using Zenject;

namespace GamePlay
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField]
        private string[] initialScenes;
        [SerializeField]
        private string[] mapScenes;
        [SerializeField]
        private string[] initialPanels;
        [SerializeField]
        private string[] otherScenes;

        private string activeMapScene;
        private SceneChangeHandler sceneChangeHandler;
        private WorldStore worldStore;
        private CanvasInitializer canvasInitializer;


        private Dictionary<string, FakeSceneLoader> fakeScenes = new Dictionary<string, FakeSceneLoader>();

        private string currentSubScene = null;

        [Inject]
        public void Construct(SceneManagerHolder sceneManagerHolder, CanvasInitializer canvasInitializer, SceneChangeHandler sceneChangeHandler, WorldStore worldStore)
        {
            this.canvasInitializer = canvasInitializer;
            this.sceneChangeHandler = sceneChangeHandler;
            this.worldStore = worldStore;

            sceneManagerHolder.D = this;
        }

        private void Awake()
        {
            canvasInitializer.SetInitialPanels(initialPanels);

            foreach (var item in GetComponents<FakeSceneLoader>())
            {
                fakeScenes.Add($"{item.sceneName}Scene", item);
            }

            foreach (var mapScene in mapScenes)
            {
                var map = new MapState();
                map.Name = $"{mapScene}Scene";
                worldStore.AddWorld(map);
            }
        }

        public void EnterSubScene(string scene)
        {
            var sceneName = $"{scene}Scene";

            if (fakeScenes.ContainsKey(sceneName))
            {
                fakeScenes[sceneName].Load();
            } else
            {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                currentSubScene = sceneName;
            }
        }

        public void ExitSubScene(string scene)
        {
            var sceneName = $"{scene}Scene";

            if (fakeScenes.ContainsKey(sceneName))
            {
                fakeScenes[sceneName].Unload();
            }
        }

        public void LoadScene(string name)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        }

        public void LoadInitialScenes()
        {
            foreach (var scene in initialScenes)
            {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync($"{scene}Scene", LoadSceneMode.Additive);
            }
        }

        public void LoadMapScene(int index)
        {
            string sceneName = $"{mapScenes[index]}Scene";
            worldStore.SetActiveWorld(sceneName);
            activeMapScene = sceneName;
            sceneChangeHandler.ClearPrevScene();
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public void UnLoadMapScene(int index)
        {
            string sceneName = $"{mapScenes[index]}Scene";
            if (activeMapScene == sceneName)
            {
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
            }
        }
    }
}
