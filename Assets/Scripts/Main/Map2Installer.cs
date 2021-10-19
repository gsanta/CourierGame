
using GUI;
using Scenes;
using UnityEngine;
using Zenject;

namespace Main
{
    public class Map2Installer : MonoInstaller, ISceneSetup
    {
        [SerializeField]
        private SceneInitializer sceneInitializer;
        [SerializeField]
        private SceneLoaderController sceneLoaderController;

        public override void InstallBindings()
        {
            Container.Bind<SceneLoaderController>().FromInstance(sceneLoaderController).AsSingle();
            Container.Bind<SceneInitializer>().FromInstance(sceneInitializer).AsSingle();

            Container.Resolve<SceneInitializer>().SetSceneSetup(this);
        }

        public void SetupScene()
        {
        }
    }
}
