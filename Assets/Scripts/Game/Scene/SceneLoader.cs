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
        private string[] otherScenes;

        private string currentSubScene = null;

        public SceneLoader(SceneChangeHandler sceneChangeHandler, WorldStore worldStore)
        {
            this.sceneChangeHandler = sceneChangeHandler;
            this.worldStore = worldStore;
        }

        public void SetScenes(string[] initialScenes, string[] mapScenes, string[] subScenes)
        {
            this.initialScenes = initialScenes;
            this.mapScenes = mapScenes;
            this.otherScenes = subScenes;

            foreach (var mapScene in mapScenes)
            {
                var map = new MapState();
                map.Name = $"{mapScene}Scene";
                worldStore.AddWorld(map);
            }
        }

        public void EnterSubScene(string name)
        {
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            currentSubScene = name;
        }

        public void ExitSubScene()
        {

        }

        public void LoadScene(string name)
        {
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
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
