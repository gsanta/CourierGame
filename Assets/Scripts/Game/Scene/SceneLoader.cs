using UnityEngine.SceneManagement;
using Worlds;

namespace Scenes
{
    public class SceneLoader
    {
        private string activeMapScene;
        private SceneChangeHandler sceneChangeHandler;
        private WorldStore worldStore;

        public SceneLoader(SceneChangeHandler sceneChangeHandler, WorldStore worldStore)
        {
            this.sceneChangeHandler = sceneChangeHandler;
            this.worldStore = worldStore;
        }

        public void LoadInitialScenes()
        {
            SceneManager.LoadSceneAsync("CanvasScene", LoadSceneMode.Additive);
        }

        public void LoadWorldScene(int index)
        {
            string sceneName = WorldState.GenerateWorldName(index);
            worldStore.SetActiveWorld(sceneName);
            activeMapScene = sceneName;
            sceneChangeHandler.ClearPrevScene();
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public void UnLoadMapScene(int index)
        {
            string sceneName = "Map" + index + "Scene";
            if (activeMapScene == sceneName)
            {
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }
    }
}
