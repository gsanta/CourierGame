using Stats;
using Zenject;

namespace Main
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITimeProvider>().To<DefaultTimeProvider>().AsSingle();
            Container.Bind<Timer>().AsSingle();
        }
    }
}
