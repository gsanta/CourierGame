using Agents;
using Bikers;
using Cameras;
using Core;
using Delivery;
using GUI;
using Minimap;
using Pedestrians;
using Route;
using Scenes;
using Stats;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private PanelController panelController;

    [SerializeField]
    private InputHandler inputHandler;

    [SerializeField]
    private PackageStoreController packageStoreController;
    [SerializeField]
    private PackageSpawnPointStoreController packageSpawnPointStoreController;
    [SerializeField]
    private PackageTargetPointStoreController packageTargetPointStoreController;
    [SerializeField]
    private PackageInstantiator packageInstantiator;

    [SerializeField]
    private BikerFactory bikerFactory;

    [SerializeField]
    private MainCamera mainCamera;

    [SerializeField]
    private PedestrianSpawner pedestrianSpawner;
    [SerializeField]
    private PedestrianFactory pedestrianFactory;
    [SerializeField]
    private PedestrianTargetStore pedestrianTargetStore;

    [SerializeField]
    private PackageStore2 packageStore2;

    [SerializeField]
    private RoadStore roadStore;
    [SerializeField]
    private PavementStore pavementStore;

    [SerializeField]
    private ReconciliationController reconciliationController;

    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<PanelController>().FromInstance(panelController).AsSingle();

        Container.Bind<DayService>().AsSingle();

        Container.Bind<BikerFactory>().FromInstance(bikerFactory).AsSingle();
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

        Container.Bind<MainCamera>().FromInstance(mainCamera).AsSingle();

        Container.Bind<PedestrianSpawner>().FromInstance(pedestrianSpawner).AsSingle();
        Container.Bind<PedestrianStore>().AsSingle();
        Container.Bind<PedestrianFactory>().FromInstance(pedestrianFactory).AsSingle();
        Container.Bind<PedestrianTargetStore>().FromInstance(pedestrianTargetStore).AsSingle();
        Container.Bind<PedestrianSetup>().AsSingle();

        Container.Bind<PackageStore2>().FromInstance(packageStore2).AsSingle();

        Container.Bind<RoadStore>().FromInstance(roadStore).AsSingle();
        Container.Bind<PavementStore>().FromInstance(pavementStore).AsSingle();
        Container.Bind<RouteFacade>().AsSingle();
        Container.Bind<RouteSetup>().AsSingle().NonLazy();

        Container.Bind<ReconciliationController>().FromInstance(reconciliationController).AsSingle();

        // actions
        Container.Bind<RouteAction>().AsSingle().NonLazy();
        Container.Bind<PickUpPackageAction>().AsSingle().NonLazy();
        Container.Bind<DeliverPackageAction>().AsSingle().NonLazy();
        Container.Bind<ReservePackageAction>().AsSingle().NonLazy();
        Container.Bind<WalkAction>().AsSingle().NonLazy();
        Container.Bind<AgentFactory>().AsSingle().NonLazy();
        Container.Bind<ActionProvider>().AsSingle().NonLazy();

        Container.Bind<PathCache>().AsSingle().NonLazy();
    }
    override public void Start()
    {
        base.Start();

        Invoke("RunSetups", 0.5f);
    }

    private void RunSetups()
    {
        BikerSetup bikerSetup = Container.Resolve<BikerSetup>();
        bikerSetup.Setup();
        PedestrianSetup pedestrianSetup = Container.Resolve<PedestrianSetup>();
        pedestrianSetup.Setup();
        Container.Resolve<DayService>();
        Container.Resolve<MinimapPackageProvider>();
        Container.Resolve<MinimapPackageConsumer>();

        AgentFactory agentFactory = Container.Resolve<AgentFactory>();
        agentFactory.AddBikerAction(Container.Resolve<RouteAction>());
        agentFactory.AddBikerAction(Container.Resolve<PickUpPackageAction>());
        agentFactory.AddBikerAction(Container.Resolve<DeliverPackageAction>());
        agentFactory.AddBikerAction(Container.Resolve<ReservePackageAction>());
        agentFactory.AddPedestrianAction(Container.Resolve<WalkAction>());

        ActionProvider actionProvider = Container.Resolve<ActionProvider>();
        actionProvider.WalkAction = Container.Resolve<WalkAction>();
        actionProvider.Init();

        PathCache pathCache = Container.Resolve<PathCache>();
        pathCache.Init();

        SceneLoader sceneLoader = Container.Resolve<SceneLoader>();
        sceneLoader.LoadInitialScenes();
    }
}
