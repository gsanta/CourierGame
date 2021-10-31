using Controls;
using UnityEngine;
using Zenject;

namespace Main
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        private ObjectClicker objectClicker;

        public override void InstallBindings()
        {
            Container.Bind<ObjectClicker>().FromInstance(objectClicker).AsSingle();
        }
    }
}
