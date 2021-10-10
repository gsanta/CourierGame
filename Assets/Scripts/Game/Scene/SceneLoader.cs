
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class SceneLoader
    {

        public void LoadInitialScenes()
        {
            SceneManager.LoadSceneAsync("CanvasScene", LoadSceneMode.Additive);
        }
    }
}
