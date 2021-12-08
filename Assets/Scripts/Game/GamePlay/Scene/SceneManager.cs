using GameObjects;
using Scenes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Worlds;

namespace GamePlay
{
    public class SceneManager
    {
        private string activeMapScene;
        private SceneChangeHandler sceneChangeHandler;
        private WorldStore worldStore;
        private SubsceneStore subsceneStore;

        public SceneManager(SceneChangeHandler sceneChangeHandler, WorldStore worldStore, SubsceneStore subsceneStore)
        {
            this.sceneChangeHandler = sceneChangeHandler;
            this.worldStore = worldStore;
            this.subsceneStore = subsceneStore;
        }

        public SceneManagerData Data { get; set; }

        public void EnterSubScene()
        {
            var sceneName = $"{subsceneStore.SubSceneId}Scene";

            if (Data.fakeScenes.ContainsKey(sceneName))
            {
                Data.fakeScenes[sceneName].Load();
            } else
            {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }

        public void ExitSubScene()
        {
            var sceneName = $"{subsceneStore.SubSceneId}Scene";

            if (Data.fakeScenes.ContainsKey(sceneName))
            {
                Data.fakeScenes[sceneName].Unload();
            }
        }

        public void LoadScene(string name)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        }

        public void LoadInitialScenes()
        {
            foreach (var scene in Data.initialScenes)
            {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync($"{scene}Scene", LoadSceneMode.Additive);
            }
        }

        public void LoadMapScene(int index)
        {
            string sceneName = $"{Data.mapScenes[index]}Scene";
            worldStore.SetActiveWorld(sceneName);
            activeMapScene = sceneName;
            sceneChangeHandler.ClearPrevScene();
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public void UnLoadMapScene(int index)
        {
            string sceneName = $"{Data.mapScenes[index]}Scene";
            if (activeMapScene == sceneName)
            {
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
            }
        }
    }
}
