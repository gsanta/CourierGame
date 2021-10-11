using Bikers;
using Delivery;
using Stats;
using UI;
using Zenject;

namespace Main
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITimeProvider>().To<DefaultTimeProvider>().AsSingle();
            Container.Bind<Timer>().AsSingle();
            Container.Bind<PanelManager>().AsSingle();
            Container.Bind<BikerStore>().AsSingle();
            Container.Bind<BikerService>().AsSingle();
            Container.Bind<RoleService>().AsSingle();
            Container.Bind<PackageStore>().AsSingle();
        }
    }
}
