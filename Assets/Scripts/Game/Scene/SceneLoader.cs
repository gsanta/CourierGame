
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class SceneLoader
    {

        public void LoadInitialScenes()
        {
            SceneManager.LoadSceneAsync("CanvasScene", LoadSceneMode.Additive);
        }

        public void LoadMapScene(int index)
        {
            SceneManager.LoadSceneAsync("Map" + index + "Scene", LoadSceneMode.Additive);
        }
    }
}
