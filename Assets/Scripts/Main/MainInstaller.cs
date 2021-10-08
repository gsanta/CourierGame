using Bikers;
using Cameras;
using Delivery;
using Minimap;
using Model;
using Pedestrians;
using Route;
using Service;
using UI;
using UnityEngine;
using Zenject;
using AI;
using Core;
using Stats;
using Agents;

public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private TimelineController timelineController;
    [SerializeField]
    private PanelManager panelManager;

    [SerializeField]
    private StartDayPanel startDayPanel;
    [SerializeField]
    private DeliveryPanel deliveryPanel;
    [SerializeField]
    private InputHandler inputHandler;

    [SerializeField]
    private PackageStore packageStore;
    [SerializeField]
    private PackageSpawnPointStore packageSpawnPointStore;
    [SerializeField]
    private PackageTargetPointStore packageTargetPointStore;
    [SerializeField]
    private PackageFactory packageFactory;

    [SerializeField]
    private BikerStore bikerStore;
    [SerializeField]
    private BikerFactory bikerFactory;
    [SerializeField]
    private BikerService bikerService;
    [SerializeField]
    private BikerPanel bikerPanel;

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
    private ReconciliationService reconciliationService;

    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<TimelineController>().FromInstance(timelineController).AsSingle();
        Container.Bind<ITimeProvider>().To<DefaultTimeProvider>().AsSingle();
        Container.Bind<Timer>().FromInstance(timer).AsSingle();
        Container.Bind<TimelineSlider>().FromInstance(timelineController.slider).AsSingle();
        Container.Bind<PanelManager>().FromInstance(panelManager).AsSingle();

        Container.Bind<DayService>().AsSingle();

        Container.Bind<BikerService>().FromInstance(bikerService).AsSingle();
        Container.Bind<BikerStore>().FromInstance(bikerStore).AsSingle();
        Container.Bind<BikerFactory>().FromInstance(bikerFactory).AsSingle();
        Container.Bind<BikerSpawner>().AsSingle();
        Container.Bind<BikerPanel>().FromInstance(bikerPanel).AsSingle();
        Container.Bind<BikerSetup>().AsSingle();
        Container.BindFactory<Object, Biker, Biker.Factory>().FromFactory<PrefabFactory<Biker>>();
        Container.BindFactory<Object, BikerPlayComponent, BikerPlayComponent.Factory>().FromFactory<PrefabFactory<BikerPlayComponent>>();
        Container.BindFactory<Object, Pedestrian, Pedestrian.Factory>().FromFactory<PrefabFactory<Pedestrian>>();

        Container.Bind<PackageStore>().FromInstance(packageStore).AsSingle();
        Container.Bind<PackageSpawnPointStore>().FromInstance(packageSpawnPointStore).AsSingle();
        Container.Bind<PackageTargetPointStore>().FromInstance(packageTargetPointStore).AsSingle();
        Container.Bind<ISpawner<PackageConfig>>().To<PackageSpawner>().AsSingle().NonLazy();
        Container.Bind<ItemFactory<PackageConfig, Package>>().To<PackageFactory>().FromInstance(packageFactory).AsSingle();
        Container.BindFactory<Object, Package, Package.Factory>().FromFactory<PrefabFactory<Package>>();

        Container.Bind<DeliveryPanel>().FromInstance(deliveryPanel).AsSingle();
        Container.Bind<DeliveryStore>().AsSingle();
        Container.Bind<IDeliveryService>().To<DeliveryService>().AsSingle();

        Container.Bind<RoleService>().AsSingle();
        Container.Bind<IEventService>().To<EventService>().AsSingle();

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

        Container.Bind<ReconciliationService>().FromInstance(reconciliationService).AsSingle();

        Container.Bind<MoneyStore>().AsSingle();

        // actions
        Container.Bind<RouteAction>().AsSingle().NonLazy();
        Container.Bind<PickUpPackageAction>().AsSingle().NonLazy();
        Container.Bind<DeliverPackageAction>().AsSingle().NonLazy();
        Container.Bind<ReservePackageAction>().AsSingle().NonLazy();
        Container.Bind<WalkAction>().AsSingle().NonLazy();
        Container.Bind<AgentFactory>().AsSingle().NonLazy();
        Container.Bind<ActionProvider>().AsSingle().NonLazy();
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
    }
}
