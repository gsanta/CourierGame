using Scenes;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class MenuPanel : MonoBehaviour
    {
        private SceneLoader sceneLoader;

        [Inject]
        public void Construct(SceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }

        public void OnLoadMap1()
        {
            this.sceneLoader.UnLoadMapScene(1);
            this.sceneLoader.LoadMapScene(0);
        }

        public void OnLoadMap2()
        {
            this.sceneLoader.UnLoadMapScene(0);
            this.sceneLoader.LoadMapScene(1);
        }
    }
}
