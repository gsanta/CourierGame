using Scenes;
using UnityEngine;
using Zenject;

namespace GUI
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
            this.sceneLoader.UnLoadMapScene(2);
            this.sceneLoader.LoadMapScene(1);
        }

        public void OnLoadMap2()
        {
            this.sceneLoader.UnLoadMapScene(1);
            this.sceneLoader.LoadMapScene(2);
        }
    }
}
