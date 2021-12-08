using Agents;
using GameObjects;
using Buildings;
using Cameras;
using Controls;
using Scenes;
using Delivery;
using Enemies;
using Pedestrians;
using Route;
using Service;
using States;
using Stats;
using UI;
using Worlds;
using Zenject;
using Actions;
using Routes;
using GamePlay;
using AI;
using GizmoNS;
using Movement;

namespace Main
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITimeProvider>().To<DefaultTimeProvider>().AsSingle();
            Container.Bind<Timer>().AsSingle().NonLazy();
            Container.Bind<CanvasStore>().AsSingle();
            Container.Bind<PlayerStore>().AsSingle().NonLazy();
            Container.Bind<CharacterStore>().AsSingle();
            Container.Bind<PackageStore>().AsSingle().NonLazy();
            Container.Bind<EventService>().AsSingle();
            Container.Bind<DeliveryService>().To<DeliveryService>().AsSingle();
            Container.Bind<MoneyStore>().AsSingle();

            Container.Bind<ReconciliationService>().AsSingle();
            Container.Bind<PackageSpawnPointStore>().AsSingle().NonLazy();
            Container.Bind<PackageTargetPointStore>().AsSingle().NonLazy();
            Container.Bind<PackageFactory>().AsSingle();
            Container.Bind<BikerFactory>().AsSingle();
            Container.Bind<PedestrianFactory>().AsSingle();
            Container.Bind<PedestrianSpawner>().AsSingle();
            Container.Bind<PedestrianGoalFactory>().AsSingle();
            Container.Bind<WalkTargetStore>().AsSingle().NonLazy();
            Container.Bind<PedestrianStore>().AsSingle().NonLazy();

            Container.Bind<AgentFactory>().AsSingle().NonLazy();
            Container.Bind<ActionStore>().AsSingle().NonLazy();
            Container.Bind<SceneChangeHandler>().AsSingle().NonLazy();
            Container.Bind<PlayPanel>().AsSingle().NonLazy();
            Container.Bind<DeliveryPanel>().AsSingle();
            Container.Bind<CameraController>().AsSingle().NonLazy();
            Container.Bind<BuildingStore>().AsSingle().NonLazy();
            Container.Bind<WorldStore>().AsSingle().NonLazy();
            Container.Bind<WorldHandlers>().AsSingle().NonLazy();
            Container.Bind<CurfewHandler>().AsSingle();
            Container.Bind<EnemiesConfig>().AsSingle();
            Container.Bind<BikersConfig>().AsSingle();
            Container.Bind<StoreSetup>().AsSingle();

            // game play
            Container.Bind<TurnManager>().AsSingle();
            Container.Bind<TurnHelper>().AsSingle();
            Container.Bind<ITurns>().WithId("PlayerPlayTurns").To<PlayerPlayTurns>().AsSingle();
            Container.Bind<ITurns>().WithId("PlayerCommandTurns").To<PlayerCommandTurns>().AsSingle();
            Container.Bind<ITurns>().WithId("PedestrianTurns").To<PedestrianTurns>().AsSingle();
            Container.Bind<ITurns>().WithId("EnemyTurns").To<EnemyTurns>().AsSingle();
            Container.Bind<GameObjectStore>().AsSingle();

            // Movement
            Container.Bind<RoadStore>().AsSingle();
            Container.Bind<GridSystem>().AsSingle();

            // Actions
            Container.Bind<RouteStore>().AsSingle();
            Container.Bind<PlayerWalkAction>().AsSingle();
            Container.Bind<PlayerWalkActionCreator>().AsSingle();
            Container.Bind<PlayerWalkIntoBuildingActionCreator>().AsSingle();
            Container.Bind<PedestrianWalkActionCreator>().AsSingle();
            Container.Bind<EnemyWalkActionCreator>().AsSingle();
            Container.Bind<ActionFactory>().AsSingle();
            Container.Bind<ActionCreators>().AsSingle();
            Container.Bind<ExitBuildingPostAction>().AsSingle();
            Container.Bind<EnterBuildingPostAction>().AsSingle();

            // ui
            Container.Bind<CurfewButton>().AsSingle();
            Container.Bind<BikerHomeStore>().AsSingle();
            Container.Bind<EnemySpawnPointStore>().AsSingle();
            Container.Bind<EnemySpawner>().AsSingle();
            Container.Bind<EnemyStore>().AsSingle();
            Container.Bind<EnemyFactory>().AsSingle();
            Container.Bind<PointerHandler>().AsSingle();
            Container.Bind<SelectionTool>().AsSingle();
            Container.Bind<CommandTool>().AsSingle();
            Container.Bind<RouteTool>().AsSingle();
            Container.Bind<SelectionBox>().AsSingle();
            Container.Bind<ObjectClicker>().AsSingle();
            Container.Bind<CanvasInitializer>().AsSingle();

            // Game object
            Container.Bind<OutlineGameObjectSelector>().AsTransient();
            Container.Bind<SubsceneStore>().AsSingle().NonLazy();
            Container.Bind<SceneManager>().AsSingle().NonLazy();


            // State
            Container.Bind<SelectionStore>().AsSingle();

            // Gizmo
            Container.Bind<ArrowRendererProvider>().AsSingle();

            SetupActionFactory();
        }

        override public void Start()
        {
            base.Start();

            Invoke("RunSetups", 0.5f);
        }

        private void RunSetups()
        {
            WorldStore worldStore = Container.Resolve<WorldStore>();
            WorldHandlers worldHandlers = Container.Resolve<WorldHandlers>();
            worldStore.SetWorldHandlers(worldHandlers);

            SceneManager sceneManager = Container.Resolve<SceneManager>();
            sceneManager.LoadInitialScenes();

            SceneChangeHandler sceneChangeHandler = Container.Resolve<SceneChangeHandler>();
            sceneChangeHandler.AddResetable(Container.Resolve<GameObjects.PlayerStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<PackageStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<PackageSpawnPointStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<PackageTargetPointStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<WalkTargetStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<PedestrianStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<RoadStore>());

            sceneChangeHandler.AddResetable(Container.Resolve<ActionStore>());
            sceneChangeHandler.AddResetable(Container.Resolve<PlayPanel>());
            sceneChangeHandler.AddResetable(Container.Resolve<DeliveryPanel>());
            sceneChangeHandler.AddResetable(Container.Resolve<Timer>());
            sceneChangeHandler.AddResetable(Container.Resolve<BuildingStore>());

            SetupControl();
        }

        private void SetupActionFactory()
        {
            var actionFactory = Container.Resolve<ActionFactory>();
            actionFactory.actionCreators.PlayerWalkIntoBuildingActionCreator = Container.Resolve<PlayerWalkIntoBuildingActionCreator>();
            actionFactory.actionCreators.PlayerWalkActionCreator = Container.Resolve<PlayerWalkActionCreator>();
            actionFactory.actionCreators.PedestrianWalkActionCreator = Container.Resolve<PedestrianWalkActionCreator>();
            actionFactory.actionCreators.EnemyWalkActionCreator = Container.Resolve<EnemyWalkActionCreator>();
            actionFactory.actionCreators.EnterBuildingPostAction = Container.Resolve<EnterBuildingPostAction>();
            actionFactory.actionCreators.ExitBuildingPostAction = Container.Resolve<ExitBuildingPostAction>();
        }

        private void SetupControl()
        {
            PointerHandler pointerHandler = Container.Resolve<PointerHandler>();
            SelectionTool selectionTool = Container.Resolve<SelectionTool>();
        }
    }
}
