using GUI;
using UnityEngine;
using Zenject;

namespace Main
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        SceneLoaderHandler sceneHandler;

        public override void InstallBindings()
        {
            Container.Bind<SceneLoaderHandler>().FromInstance(sceneHandler).AsSingle();
        }
    }
}
