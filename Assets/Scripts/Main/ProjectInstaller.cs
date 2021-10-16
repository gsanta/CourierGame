using Agents;
using Bikers;
using Core;
using Delivery;
using Model;
using Pedestrians;
using Route;
using Scenes;
using Service;
using Stats;
using UI;
using UnityEngine;
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
            Container.Bind<IEventService>().To<EventService>().AsSingle();
            Container.Bind<IDeliveryService>().To<DeliveryService>().AsSingle();
            Container.Bind<MoneyStore>().AsSingle();

            Container.Bind<ReconciliationService>().AsSingle();
            Container.Bind<SceneLoader>().AsSingle().NonLazy();
            Container.Bind<PackageSpawnPointStore>().AsSingle();
            Container.Bind<PackageTargetPointStore>().AsSingle();
            Container.Bind<PackageFactory>().AsSingle();
            Container.Bind<BikerFactory>().AsSingle();
            Container.Bind<PedestrianFactory>().AsSingle();
            Container.Bind<PedestrianSpawner>().AsSingle();
            Container.Bind<PedestrianTargetStore>().AsSingle().NonLazy();
            Container.Bind<PedestrianStore>().AsSingle();
            Container.Bind<RoadStore>().AsSingle();
            Container.Bind<PavementStore>().AsSingle();
            Container.Bind<AgentFactory>().AsSingle().NonLazy();
            Container.Bind<ActionProvider>().AsSingle().NonLazy();
            Container.Bind<InputHandler>().AsSingle();
        }
    }
}
