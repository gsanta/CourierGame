
using Controls;
using Scenes;
using UnityEngine;
using Zenject;

namespace Main
{
    public class Map2Installer : MonoInstaller, ISceneSetup
    {
        [SerializeField]
        private SceneInitializer sceneInitializer;

        public override void InstallBindings()
        {
            Container.Bind<SceneInitializer>().FromInstance(sceneInitializer).AsSingle();

            Container.Resolve<SceneInitializer>().SetSceneSetup(this);
        }

        public void SetupScene()
        {
        }
    }
}
