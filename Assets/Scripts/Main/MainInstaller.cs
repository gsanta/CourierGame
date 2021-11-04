using Controls;
using UnityEngine;
using Zenject;

namespace Main
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        private MouseController mouseController;

        public override void InstallBindings()
        {
            Container.Bind<MouseController>().FromInstance(mouseController).AsSingle();
        }
    }
}
