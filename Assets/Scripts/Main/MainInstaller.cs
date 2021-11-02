using GUI;
using UnityEngine;
using Zenject;

namespace Main
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        private ObjectClicker objectClicker;
        [SerializeField]
        private MouseController mouseController;

        public override void InstallBindings()
        {
            Container.Bind<ObjectClicker>().FromInstance(objectClicker).AsSingle();
            Container.Bind<MouseController>().FromInstance(mouseController).AsSingle();
        }
    }
}
