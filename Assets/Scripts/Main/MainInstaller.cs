using GUI;
using UnityEngine;
using Zenject;

namespace Main
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        SceneHandler sceneHandler;

        public override void InstallBindings()
        {
            Container.Bind<SceneHandler>().FromInstance(sceneHandler).AsSingle();
        }
    }
}
