using Agents;
using Bikers;
using Cameras;
using Delivery;
using GUI;
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
    private BikerInstantiator bikerInstantiator;
    [SerializeField]
    private CameraController mainCamera;
    [SerializeField]
    private PedestrianInstantiator pedestrianInstantiator;
    [SerializeField]
    private PedestrianTargetStoreController pedestrainTargetStoreController;
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


    public override void InstallBindings()
    {
        Container.Bind<InputHandlerController>().FromInstance(inputHandlerController).AsSingle();
        Container.Bind<PanelController>().FromInstance(panelController).AsSingle();

        Container.Bind<DayService>().AsSingle();

        Container.Bind<BikerSpawner>().AsSingle();
        Container.Bind<BikerSetup>().AsSingle();
        Container.BindFactory<Object, Biker, Biker.Factory>().FromFactory<PrefabFactory<Biker>>();
        Container.BindFactory<Object, BikerPlayComponent, BikerPlayComponent.Factory>().FromFactory<PrefabFactory<BikerPlayComponent>>();
        Container.BindFactory<Object, Pedestrian, Pedestrian.Factory>().FromFactory<PrefabFactory<Pedestrian>>();
        Container.Bind<BikerInstantiator>().FromInstance(bikerInstantiator).AsSingle();

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

        Container.Bind<IPedestrianInstantiator>().To<PedestrianInstantiator>().FromInstance(pedestrianInstantiator).AsSingle();
        Container.Bind<PedestrianSetup>().AsSingle();
        Container.Bind<PedestrianTargetStoreController>().FromInstance(pedestrainTargetStoreController).AsSingle();
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
        Container.Bind<WalkAction>().AsSingle().NonLazy();
        Container.Bind<GoHomeAction>().AsSingle().NonLazy();

        Container.Bind<PathCache>().AsSingle().NonLazy();
        Container.Bind<BuildingStoreController>().AsSingle();

        Container.Resolve<SceneInitializer>().SetSceneSetup(this);


        AgentFactory agentFactory = Container.Resolve<AgentFactory>();
        agentFactory.AddBikerAction(Container.Resolve<PickUpPackageAction>());
        agentFactory.AddBikerAction(Container.Resolve<DeliverPackageAction>());
        agentFactory.AddBikerAction(Container.Resolve<ReservePackageAction>());
        agentFactory.AddPedestrianAction(Container.Resolve<WalkAction>());
    }

    public void SetupScene()
    {
        BikerSetup bikerSetup = Container.Resolve<BikerSetup>();
        bikerSetup.Setup();
        PedestrianSetup pedestrianSetup = Container.Resolve<PedestrianSetup>();
        pedestrianSetup.Setup();
        Container.Resolve<DayService>();
        Container.Resolve<MinimapPackageProvider>();
        Container.Resolve<MinimapPackageConsumer>();

        ActionStore actionStore = Container.Resolve<ActionStore>();
        actionStore.WalkAction = Container.Resolve<WalkAction>();
        actionStore.AddPedestrianAction(Container.Resolve<GoHomeAction>());
        actionStore.Init();

        PedestrianTargetStore pedestrianTargetStore = Container.ParentContainers[0].Resolve<PedestrianTargetStore>();
        PathCache pathCache = Container.Resolve<PathCache>();
        pathCache.SetPedestrianTargetStore(pedestrianTargetStore);
        pathCache.Init();
    }

    //override public void Start()
    //{
    //    base.Start();

    //    Invoke("RunSetups", 0.5f);
    //}
}
