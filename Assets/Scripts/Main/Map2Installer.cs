
using GUI;
using UnityEngine;
using Zenject;

namespace Main
{
    public class Map2Installer : MonoInstaller
    {
        [SerializeField]
        private SceneInitializer sceneInitializer;
        [SerializeField]
        private SceneLoaderController sceneLoaderController;

        public override void InstallBindings()
        {
            Container.Bind<SceneLoaderController>().FromInstance(sceneLoaderController).AsSingle();
            Container.Bind<SceneInitializer>().FromInstance(sceneInitializer).AsSingle();
        }
    }
}
