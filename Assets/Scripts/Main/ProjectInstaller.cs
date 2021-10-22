using Agents;
using Bikers;
using Cameras;
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
            Container.Bind<Timer>().AsSingle().NonLazy();
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
            Container.Bind<CameraHandler>().AsSingle().NonLazy();
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
            sceneChangeHandler.AddResetable(Container.Resolve<BikerStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<PackageStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<PackageSpawnPointStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<PackageTargetPointStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<PedestrianTargetStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<PedestrianStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<RoadStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<PavementStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<ActionStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<BikerPanel>());
            sceneChangeHandler.AddResetable(Container.Resolve<DeliveryPanel>());
            sceneChangeHandler.AddResetable(Container.Resolve<Timer>());
            sceneChangeHandler.AddResetable(Container.Resolve<CameraHandler>());
        }
    }
}
