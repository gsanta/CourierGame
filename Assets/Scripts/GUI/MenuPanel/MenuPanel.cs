using GamePlay;
using Scenes;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class MenuPanel : MonoBehaviour
    {
        private SceneManagerHolder sceneManager;

        [Inject]
        public void Construct(SceneManagerHolder sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        public void OnLoadMap1()
        {
            this.sceneManager.D.UnLoadMapScene(1);
            this.sceneManager.D.LoadMapScene(0);
        }

        public void OnLoadMap2()
        {
            this.sceneManager.D.UnLoadMapScene(0);
            this.sceneManager.D.LoadMapScene(1);
        }
    }
}
