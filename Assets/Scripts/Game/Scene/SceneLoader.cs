using UnityEngine.SceneManagement;
using Worlds;

namespace Scenes
{
    public class SceneLoader
    {
        private string activeMapScene;
        private SceneChangeHandler sceneChangeHandler;
        private WorldStore worldStore;

        private string[] initialScenes;
        private string[] mapScenes;

        public SceneLoader(SceneChangeHandler sceneChangeHandler, WorldStore worldStore)
        {
            this.sceneChangeHandler = sceneChangeHandler;
            this.worldStore = worldStore;
        }

        public void SetScenes(string[] initialScenes, string[] mapScenes)
        {
            this.initialScenes = initialScenes;
            this.mapScenes = mapScenes;

            foreach (var mapScene in mapScenes)
            {
                var map = new MapState();
                map.Name = $"{mapScene}Scene";
                worldStore.AddWorld(map);
            }
        }

        public void LoadInitialScenes()
        {
            foreach (var scene in initialScenes)
            {
                SceneManager.LoadSceneAsync($"{scene}Scene", LoadSceneMode.Additive);
            }
        }

        public void LoadMapScene(int index)
        {
            string sceneName = $"{mapScenes[index]}Scene";
            worldStore.SetActiveWorld(sceneName);
            activeMapScene = sceneName;
            sceneChangeHandler.ClearPrevScene();
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public void UnLoadMapScene(int index)
        {
            string sceneName = $"{mapScenes[index]}Scene";
            if (activeMapScene == sceneName)
            {
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }
    }
}
