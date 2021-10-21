using Agents;
using Bikers;
using Core;
using Delivery;
using Pedestrians;
using Route;
using Scenes;
using Service;
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
            Container.Bind<PanelStore>().AsSingle();
            Container.Bind<BikerStore>().AsSingle().NonLazy();
            Container.Bind<BikerService>().AsSingle();
            Container.Bind<RoleService>().AsSingle();
            Container.Bind<PackageStore>().AsSingle().NonLazy();
            Container.Bind<EventService>().AsSingle();
            Container.Bind<DeliveryService>().To<DeliveryService>().AsSingle();
            Container.Bind<MoneyStore>().AsSingle();

            Container.Bind<ReconciliationService>().AsSingle();
            Container.Bind<SceneLoader>().AsSingle().NonLazy();
            Container.Bind<PackageSpawnPointStore>().AsSingle().NonLazy();
            Container.Bind<PackageTargetPointStore>().AsSingle().NonLazy();
            Container.Bind<PackageFactory>().AsSingle();
            Container.Bind<BikerFactory>().AsSingle();
            Container.Bind<PedestrianFactory>().AsSingle();
            Container.Bind<PedestrianSpawner>().AsSingle();
            Container.Bind<PedestrianTargetStore>().AsSingle().NonLazy();
            Container.Bind<PedestrianStore>().AsSingle().NonLazy();
            Container.Bind<RoadStore>().AsSingle().NonLazy();
            Container.Bind<PavementStore>().AsSingle().NonLazy();
            Container.Bind<AgentFactory>().AsSingle().NonLazy();
            Container.Bind<ActionStore>().AsSingle().NonLazy();
            Container.Bind<InputHandler>().AsSingle();
            Container.Bind<SceneChangeHandler>().AsSingle().NonLazy();
            Container.Bind<BikerPanel>().AsSingle().NonLazy();
            Container.Bind<DeliveryPanel>().AsSingle();
        }

        override public void Start()
        {
            base.Start();

            Invoke("RunSetups", 0.5f);
        }

        private void RunSetups()
        {
            SceneLoader sceneLoader = Container.Resolve<SceneLoader>();
            sceneLoader.LoadInitialScenes();

            SceneChangeHandler sceneChangeHandler = Container.Resolve<SceneChangeHandler>();
            sceneChangeHandler.AddClearableStore(Container.Resolve<BikerStore>());
            sceneChangeHandler.AddClearableStore(Container.Resolve<PackageStore>());
            sceneChangeHandler.AddClearableStore(Container.Resolve<PackageSpawnPointStore>());
            sceneChangeHandler.AddClearableStore(Container.Resolve<PackageTargetPointStore>());
            sceneChangeHandler.AddClearableStore(Container.Resolve<PedestrianTargetStore>());
            sceneChangeHandler.AddClearableStore(Container.Resolve<PedestrianStore>());
            sceneChangeHandler.AddClearableStore(Container.Resolve<RoadStore>());
            sceneChangeHandler.AddClearableStore(Container.Resolve<PavementStore>());
            sceneChangeHandler.AddClearableStore(Container.Resolve<ActionStore>());
            sceneChangeHandler.AddClearableStore(Container.Resolve<BikerPanel>());
        }
    }
}
