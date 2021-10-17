using UnityEngine.SceneManagement;

namespace Scenes
{
    public class SceneLoader
    {
        private string activeMapScene;
        private SceneChangeHandler sceneChangeHandler;

        public SceneLoader(SceneChangeHandler sceneChangeHandler)
        {
            this.sceneChangeHandler = sceneChangeHandler;
        }

        public void LoadInitialScenes()
        {
            SceneManager.LoadSceneAsync("CanvasScene", LoadSceneMode.Additive);
        }

        public void LoadMapScene(int index)
        {
            string sceneName = "Map" + index + "Scene";
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
