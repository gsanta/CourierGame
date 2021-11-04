using Agents;
using Bikers;
using Buildings;
using Delivery;
using Enemies;
using Controls;
using Minimap;
using Pedestrians;
using Route;
using Scenes;
using Stats;
using UnityEngine;
using Zenject;

public class Map1Installer : MonoInstaller, ISceneSetup
{
    [SerializeField]
    private PanelController panelController;
    [SerializeField]
    private InputHandlerController inputHandlerController;
    [SerializeField]
    private PackageStoreController packageStoreController;
    [SerializeField]
    private PackageSpawnPointStoreController packageSpawnPointStoreController;
    [SerializeField]
    private PackageTargetPointStoreController packageTargetPointStoreController;
    [SerializeField]
    private PackageInstantiator packageInstantiator;
    [SerializeField]
    private CameraController mainCamera;
    [SerializeField]
    private PedestrainConfigController pedestrainConfigController;
    [SerializeField]
    private RoadStoreController roadStoreController;
    [SerializeField]
    private PavementStoreController pavementStoreController;
    [SerializeField]
    private ReconciliationController reconciliationController;
    [SerializeField]
    private SceneLoaderController sceneLoaderController;
    [SerializeField]
    private SceneInitializer sceneInitializer;
    [SerializeField]
    private TimerController timerController;
    [SerializeField]
    private BuildingStoreController buildingStoreController;
    
    [SerializeField]
    private TargetStoreController walkTargetStoreController;

    [SerializeField]
    private BikersConfigController bikersConfigController;

    [SerializeField]
    private EnemiesConfigController enemiesConfigController;

    public override void InstallBindings()
    {
        Container.Bind<InputHandlerController>().FromInstance(inputHandlerController).AsSingle();
        Container.Bind<PanelController>().FromInstance(panelController).AsSingle();

        Container.Bind<DayService>().AsSingle();

        Container.Bind<BikersConfigController>().FromInstance(bikersConfigController).AsSingle();
        Container.Bind<BikerSpawner>().AsSingle();
        Container.Bind<BikerSetup>().AsSingle();
        Container.BindFactory<Object, Biker, Biker.Factory>().FromFactory<PrefabFactory<Biker>>();
        Container.BindFactory<Object, BikerPlayComponent, BikerPlayComponent.Factory>().FromFactory<PrefabFactory<BikerPlayComponent>>();
        Container.BindFactory<Object, Pedestrian, Pedestrian.Factory>().FromFactory<PrefabFactory<Pedestrian>>();

        Container.Bind<PackageStoreController>().FromInstance(packageStoreController).AsSingle();
        Container.Bind<PackageSpawnPointStoreController>().FromInstance(packageSpawnPointStoreController).AsSingle();
        Container.Bind<PackageTargetPointStoreController>().FromInstance(packageTargetPointStoreController).AsSingle();
        Container.Bind<ISpawner<PackageConfig>>().To<PackageSpawner>().AsSingle().NonLazy();
        Container.Bind<PackageInstantiator>().FromInstance(packageInstantiator).AsSingle();
        Container.BindFactory<Object, Package, Package.Factory>().FromFactory<PrefabFactory<Package>>();

        Container.Bind<DeliveryStore>().AsSingle();

        Container.Bind<MinimapStore>().AsSingle();
        Container.Bind<MinimapPackageProvider>().AsSingle();
        Container.Bind<MinimapPackageConsumer>().AsSingle();

        Container.Bind<CameraController>().FromInstance(mainCamera).AsSingle();

        Container.Bind<PedestrianSetup>().AsSingle();
        Container.Bind<PedestrainConfigController>().FromInstance(pedestrainConfigController).AsSingle();

        Container.Bind<RoadStoreController>().FromInstance(roadStoreController).AsSingle();
        Container.Bind<PavementStoreController>().FromInstance(pavementStoreController).AsSingle();
        Container.Bind<RouteFacade>().AsSingle();
        Container.Bind<RouteSetup>().AsSingle().NonLazy();

        Container.Bind<ReconciliationController>().FromInstance(reconciliationController).AsSingle();

        Container.Bind<SceneLoaderController>().FromInstance(sceneLoaderController).AsSingle();
        Container.Bind<SceneInitializer>().FromInstance(sceneInitializer).AsSingle().NonLazy();
        Container.Bind<TimerController>().FromInstance(timerController).AsSingle();

        // actions
        Container.Bind<PickUpPackageAction>().AsSingle().NonLazy();
        Container.Bind<DeliverPackageAction>().AsSingle().NonLazy();
        Container.Bind<ReservePackageAction>().AsSingle().NonLazy();
        Container.Bind<WalkAction<Pedestrian>>().AsSingle().NonLazy();
        Container.Bind<WalkAction<Enemy>>().AsSingle().NonLazy();
        Container.Bind<GoHomeAction>().AsSingle().NonLazy();
        Container.Bind<PedestrianActionCreator>().AsSingle();
        Container.Bind<EnemyActionCreator>().AsSingle();

        Container.Bind<PathCache>().AsSingle().NonLazy();
        Container.Bind<BuildingStoreController>().AsSingle();

        Container.Bind<EnemiesConfigController>().FromInstance(enemiesConfigController).AsSingle();
        Container.Bind<EnemySetup>().AsSingle();
        //Container.Bind<EnemyInstantiator>().FromInstance(enemyInstantiator).AsSingle();

        Container.Resolve<SceneInitializer>().SetSceneSetup(this);

        AgentFactory agentFactory = Container.Resolve<AgentFactory>();
        agentFactory.AddBikerAction(Container.Resolve<PickUpPackageAction>());
        agentFactory.AddBikerAction(Container.Resolve<DeliverPackageAction>());
        agentFactory.AddBikerAction(Container.Resolve<ReservePackageAction>());
    }

    public void SetupScene()
    {
        BikerHomeStore bikerHomeStore = Container.Resolve<BikerHomeStore>();
        bikersConfigController.GetComponent<TargetStoreController>().SetupStoreWithGameObjects(bikerHomeStore);

        EnemiesConfigController enemiesConfigController = Container.Resolve<EnemiesConfigController>();

        EnemySpawnPointStore enemySpawnPointStore = Container.Resolve<EnemySpawnPointStore>();
        enemiesConfigController.GetComponent<TargetStoreController>().SetupStoreWithGameObjects(enemySpawnPointStore);

        WalkTargetStore walkTargetStore = Container.Resolve<WalkTargetStore>();
        walkTargetStoreController.SetupStore(walkTargetStore);

        EnemiesConfig enemiesConfig = Container.Resolve<EnemiesConfig>();
        enemiesConfigController.SetupConfig(enemiesConfig);

        Container.Resolve<DayService>();
        Container.Resolve<MinimapPackageProvider>();
        Container.Resolve<MinimapPackageConsumer>();

        PathCache pathCache = Container.Resolve<PathCache>();
        pathCache.SetWalkTargetStore(walkTargetStore);
        pathCache.Init();

        RouteFacade routeFacade = Container.Resolve<RouteFacade>();
        ActionStore actionStore = Container.Resolve<ActionStore>();
        actionStore.SetPedestrianActionCreator(Container.Resolve<PedestrianActionCreator>());
        actionStore.SetEnemyActionCreator(Container.Resolve<EnemyActionCreator>());
        BuildingStore buildingStore = Container.Resolve<BuildingStore>();

        actionStore.SetEnemyWalkAction(new WalkAction<Enemy>(routeFacade, walkTargetStore, pathCache));
        actionStore.SetPedestrianWalkAction(new WalkAction<Pedestrian>(routeFacade, walkTargetStore, pathCache));
        actionStore.SetGoHomeAction(new GoHomeAction(routeFacade, pathCache, buildingStore));

        BikerSetup bikerSetup = Container.Resolve<BikerSetup>();
        bikerSetup.Setup();
        PedestrianSetup pedestrianSetup = Container.Resolve<PedestrianSetup>();
        pedestrianSetup.Setup();
        EnemySetup enemySetup = Container.Resolve<EnemySetup>();
        enemySetup.Setup();
    }
}
