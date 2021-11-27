using GamePlay;
using UnityEngine;
using Zenject;

namespace Main
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        private SceneManager sceneManager;

        public override void InstallBindings()
        {
            Container.Bind<SceneManager>().FromInstance(sceneManager).AsSingle();
        }
    }
}
