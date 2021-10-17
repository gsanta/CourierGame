
using Scenes;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class SceneLoaderController : MonoBehaviour
    {
        private SceneChangeHandler sceneChangeHandler;

        [Inject]
        public void Construct(SceneChangeHandler sceneChangeHandler)
        {
            this.sceneChangeHandler = sceneChangeHandler;
        }

        private void Start()
        {
            sceneChangeHandler.InitCurrentScene();
        }
    }
}
